using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;                 //for saving images or files to server
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace CutListRepositoryPatternMVC.Areas.Admin.Controllers
{
    //user
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //for dependency injection need IWebHOst to upload images or files to server
        private readonly IWebHostEnvironment _hostEnvironment;


        //binding the viewmodel properties automatically.
        [BindProperty]
        public ServiceViewModel ServVM { get; set; }

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            //create ViewModel to pass the info securly
            //REMOVED AS BINDED FOR THE CLASS... ServiceViewModel ServVM = new ServiceViewModel()
            ServVM = new ServiceViewModel()
            {   
                //adding service
                //populating the drop down lists
                Service = new CutList.Models.Service(),         //IS IT OK TO ACCESS CUTLIST MODEL DIRECTLY HERE???
                JobList = _unitOfWork.Job.GetJobListForDropDown(),
                FrequencyList = _unitOfWork.Frequency.GetFrequencyForDropdown(),
            };
            //editing service
            if(id != null)
            {   
                //retrieve service from database to give to view
                ServVM.Service = _unitOfWork.Service.Get(id.GetValueOrDefault());
            }
            return View(ServVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //no need to pass serviceViewModel as is binded for the class
        public IActionResult Upsert()
        {
            if(ModelState.IsValid)
            {
                //file uploaded is pushed here (www.root\filepath...)

                //file path of the host server root folder
                string webRootPath = _hostEnvironment.WebRootPath;

                //retrieve file uploaded with the form
                var files = HttpContext.Request.Form.Files;
                //if new service
                if(ServVM.Service.Id == 0)
                {
                    //----------- new service -------------
                    //("Guid" Global Unique Identifier https://docs.microsoft.com/en-us/dotnet/api/system.guid.newguid?view=netcore-3.1)
                    string fileName = Guid.NewGuid().ToString();
                    //concate a file path www.root\images\services https://docs.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=netcore-3.1
                    var uploads = Path.Combine(webRootPath, @"images\services");

                    //NOTE: checking that user has uploaded file is done inside the view with JS

                    //Upload one file
                    var extension = Path.GetExtension(files[0].FileName);

                    //combine uploads with filename and extension giving us final file path to use
                    //create new file (if already exists it will be over loaded but we have Guid in fileName variable so this won't happen)
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName+extension), FileMode.Create))
                    {
                        //copy uploaded file to the filepath (grid plus same extension)
                        files[0].CopyTo(fileStreams);
                    }
                    //save the filepath of file to the database
                    ServVM.Service.ImageUrl = @"\images\services\" + fileName + extension;
                    _unitOfWork.Service.Add(ServVM.Service);
                }//if
                else
                {
                    //--------- Edit Service ---------
                    //get the service from database by id
                    var serviceFromDb = _unitOfWork.Service.Get(ServVM.Service.Id);
                    //if no file count is more than zero
                    if(files.Count > 0)
                    {   
                        //unique number
                        string fileName = Guid.NewGuid().ToString();
                        //concate a file path www.root\images\services https://docs.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=netcore-3.1
                        var uploads = Path.Combine(webRootPath, @"images\services");
                        //Upload one file
                        var extension_replacing = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
                        //if image file exists
                        if(System.IO.File.Exists(imagePath))
                        {
                            //delete old file in order to replace it
                            System.IO.File.Delete(imagePath);
                        }

                        //file path to use
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_replacing), FileMode.Create))
                        {
                            //copy replacement uploaded file to the filepath (grid plus same extension)
                            files[0].CopyTo(fileStreams);
                        }
                        //save the filepath of replacment file to the database
                        ServVM.Service.ImageUrl = @"\images\services\" + fileName + extension_replacing;
                    }
                    else
                    {
                        //the URL from database (no other changes)
                        ServVM.Service.ImageUrl = serviceFromDb.ImageUrl;
                    }//else
                    //update the Service from ViewModel
                    _unitOfWork.Service.Update(ServVM.Service);
                }//else

                 //save to databse
                _unitOfWork.Save();
                //return to page
                return RedirectToAction(nameof(Index));

            }//if
            else
            {
                //ensure list is full before sending to view incase model view is not valid
                ServVM.JobList = _unitOfWork.Job.GetJobListForDropDown();
                ServVM.FrequencyList = _unitOfWork.Frequency.GetFrequencyForDropdown();
                //return to view if model state not valid
                return View(ServVM);
            }//else
        }
       

        #region API calls

        public IActionResult GetAll()
        {
            // eger loading, key bindings on GetAll (allowing drill down)
            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Job,Frequency") });
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.Service.Get(id);
            //delete image related to service

            //file path of the host server root folder
            string webRootPath = _hostEnvironment.WebRootPath;
            //if file exists delete
            var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
            //if image file exists
            if (System.IO.File.Exists(imagePath))
            {
                //delete old file in order to replace it
                System.IO.File.Delete(imagePath);
            }

            //if no service show error message or show success message
            if(serviceFromDb == null)
            {
                return Json(new { success=false, message="Error in attempting to delete 123"});
            }
            //remove from DB
            _unitOfWork.Service.Remove(serviceFromDb);
            _unitOfWork.Save();
            //success message
            return Json(new { success = true, message = "Deleted successfully" });
        }

        #endregion
    }
}