using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.Models;
using Microsoft.AspNetCore.Mvc;

namespace CutListRepositoryPatternMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public JobController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //not passing info from the controller, using dataTables to retrieve data
            return View();
        }

        //insert or update
        public IActionResult Upsert(int? id)    //nullable
        {
            Job job = new Job();
            //if it is an insert function
            if(id == null)
            {
                //empty object
                return View(job);
            }
            job = _unitOfWork.Job.Get(id.GetValueOrDefault());     //used as it is nullable <T> or value of id
            //id is not correct
            if(job == null)
            {
                //DECIDE WHERE TO GO WHEN NOT FOUND
                return NotFound();
            }
            return View(job);
        }



        #region API calls ---------

        [HttpGet]
        public IActionResult GetAll()
        {
            //pass the Json object
            //use the GetAll method in the Interface
            return Json(new { data = _unitOfWork.Job.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objectFromDb = _unitOfWork.Job.Get(id);
            if(objectFromDb == null)
            {
                //when success equals false the error message below is shown
                return Json(new { success = false, message = "An Error has occured when deleting" });
            }
            //put to the database
            _unitOfWork.Job.Remove(objectFromDb);
            _unitOfWork.Save();
            //when success equals true the completed message below is shown
            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion
    }
}