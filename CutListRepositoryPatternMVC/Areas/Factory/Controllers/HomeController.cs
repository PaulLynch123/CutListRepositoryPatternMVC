using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CutListRepositoryPatternMVC.Models;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using CutList.Utility;
using CutListRepositoryPatternMVC.Extensions;

namespace CutListRepositoryPatternMVC.Controllers
{
    //to identify the relevant login area folder
    [Area("Factory")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //access Db
        private readonly IUnitOfWork _unitOfWork;
        //HomeViewModel object
        private HomeViewModel HomeVM;

        //constructor to get IUnitOfWork
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /*
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        */

        public IActionResult Index()
        {
            //create HomeViewModel
            HomeVM = new HomeViewModel()
            {
                //get info for HomeView
                JobList = _unitOfWork.Job.GetAll(),
                ServiceList = _unitOfWork.Service.GetAll(includeProperties: "Frequency")
            };
            //return the HomeViewModel
            return View(HomeVM);
        }

        public IActionResult Details(int id)
        {
            //get by id, filtered by 
            var serviceFromDb = _unitOfWork.Service.GetFirstOrDefault(includeProperties: "Job,Frequency", filter: j => j.Id == id);
            return View(serviceFromDb);
        }


        public IActionResult AddToCart(int serviceId)
        {
            List<int> sessionList = new List<int>();
            //if nothing in the session cart
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(StaticDetails.SessionCart)))
            {
                //add seviceId
                sessionList.Add(serviceId);
                //set object with extension method putting it in SessionCart as a string
                HttpContext.Session.SetObject(StaticDetails.SessionCart, sessionList);
            }
            //if there are things in the cart/session already
            else
            {
                //Get the session. (http Context for the current controller action)
                //convert to List of type int
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
                //id serviceId is not in the sessionList
                if (!sessionList.Contains(serviceId))
                {
                    //add serviceId to session list
                    sessionList.Add(serviceId);
                    //reload session object back
                    HttpContext.Session.SetObject(StaticDetails.SessionCart, sessionList);
                }
            }//else
             //retrun to Index page
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
