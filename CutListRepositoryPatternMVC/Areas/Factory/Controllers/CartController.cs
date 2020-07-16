using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository;
using CutList.DataAccess.Data.Repository.IRepository;
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
        private readonly IUnitOfWork _unitOfWork;

        //bind the properties within the class for this ViewModel
        //no need to write in the parameters what you are binding
        [BindProperty]
        public CartViewModel CartVM { get; set; }

        //constructor
        public CartController(IUnitOfWork unitOfWork)
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
                    CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Job"));
                }
            }
            //retrun carVM so we can show what we have purchased
            return View(CartVM);
        }


        //delete button on the Cart for each service
        public IActionResult Remove(int serviceId)
        {
            List<int> sessionList = new List<int>();
            //retrieve as in list
            sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
            //remove the ServiceID from that sessionList
            sessionList.Remove(serviceId);
            //set session again
            HttpContext.Session.SetObject(StaticDetails.SessionCart, sessionList);

            //go back to Index page
            return RedirectToAction(nameof(Index));
        }

        //SAME AS INDEX
        public IActionResult Summary()
        {
            //if there are values in the session
            if (HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                //retrieve as in list
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
                //fill the service list in CartVM object with all details of services matching the service Id from session
                //include the frequency and category to use to show to user
                foreach (int serviceId in sessionList)
                {
                    CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Job"));
                }
            }
            //retrun carVM so we can show what we have purchased
            return View(CartVM);
        }


        //POST for summary action
        [HttpPost]
        [ValidateAntiForgeryToken]
        //ability to change the action name recognise by ASP.NET Core. So action will be summary for POst method
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            //if there are values in the session
            if (HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                //retrieve as in list
                sessionList = HttpContext.Session.GetObject<List<int>>(StaticDetails.SessionCart);
                //initialise service List before use
                CartVM.ServiceList = new List<Service>();
                //fill the service list in CartVM object with all details of services matching the service Id from session
                //include the frequency and category to use to show to user
                foreach (int serviceId in sessionList)
                {
                    //I PROBABLY DON'T NEED THE INCLUDED PROPERTIES
                    //CartVM.ServiceList.Add(_unitOfWork.Service.GetFirstOrDefault(u => u.Id == serviceId, includeProperties: "Frequency,Job"));
                    CartVM.ServiceList.Add(_unitOfWork.Service.Get(serviceId));
                }
            }

            //
            if(!ModelState.IsValid)
            {
                //if not valid return details to view again
                return View(CartVM);
            }
            else
            {   
                //if model valid
                //set details not in form
                CartVM.OrderHeader.OrderDate = DateTime.Now;
                CartVM.OrderHeader.Status = StaticDetails.StatusSubmitted;          //submitted
                CartVM.OrderHeader.ServiceCount = CartVM.ServiceList.Count;
                //save to database
                _unitOfWork.OrderHeader.Add(CartVM.OrderHeader);
                _unitOfWork.Save();
                //saving to database gives us the OrderHeaderId

                foreach(var item in CartVM.ServiceList)
                {
                    //create the order details object
                    OrderDetails orderDetails = new OrderDetails
                    {
                        ServiceId = item.Id,
                        OrderHeaderId = CartVM.OrderHeader.Id,
                        //setting the name and price from the session info rather than current database details incase they have changed since order placed
                        ServiceName = item.Name,
                        Price = item.Price
                    };
                    //add to unit of work in the loop
                    _unitOfWork.OrderDetails.Add(orderDetails);
                    
                }
                //save to database once
                _unitOfWork.Save();

                //empty session of order
                HttpContext.Session.SetObject(StaticDetails.SessionCart, new List<int>());
                //return to... (action name, controller name, route values object)
                return RedirectToAction("OrderConfirmation", "Cart", new { id = CartVM.OrderHeader.Id });
            }
        }

        //follows on from SummaryPost action
        public IActionResult OrderConfirmation(int id)
        {
            //retrun view with id that was created (CarVM,OrerHeader.Id)
            return View(id);
        }

    }
}