using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository;
using CutList.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;                 //for saving images or files to server
using Microsoft.AspNetCore.Mvc;

namespace CutListRepositoryPatternMVC.Areas.Admin.Controllers
{
    //user
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //for dependency injection need IWebHOst to upload images or files to server
        private readonly IWebHostEnvironment _hostEnvironment;

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;

        }

        public IActionResult Index()
        {
            return View();
        }



        #region API calls

        public IActionResult GetAll()
        {
            // eger loading, key bindings on GetAll (allowing drill down)
            return Json(new { data = _unitOfWork.Service.GetAll(includeProperties: "Job,Frequency") });
        }


        #endregion
    }
}