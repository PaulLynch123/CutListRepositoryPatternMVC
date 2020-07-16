using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository;
using CutList.Models;
using CutList.Models.ViewModels;
using CutList.Utility;
using CutListRepositoryPatternMVC.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CutListRepositoryPatternMVC.Areas.Factory.Controllers
{
    [Area("Factory")]
    public class CartController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        //bind the properties within the class for this ViewModel
        //no need to write in the parameters what you are binding
        [BindProperty]
        public CartViewModel CartVM { get; set; }

        //constructor
        public CartController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //assign viewmodel object
            //THIS MIGHT OTHERWISE THROW AN EXCEPTION WHEN NOT ASSIGNED
            CartVM = new CartViewModel()
            {
                OrderHeader = new OrderHeader(),
                ServiceList = new List<Service>()
            };
        }

        public IActionResult Index()
        {
            //if there are values in the session
            if(HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                //retrieve as in list
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
                //fill the service list in CartVM object with all details of services matching the service Id from session
                //include the frequency and category to use to show to user
                foreach(int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Category"));
                }
            }
            //retrun carVM so we can show what we have purchased
            return View(CartVM);
        }
    }
}