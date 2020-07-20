using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.Models.ViewModels;
using CutList.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutListRepositoryPatternMVC.Areas.Admin.Controllers
{
    [Authorize]
    //login rights
    [Area("Admin")]
    public class OrderController : Controller
    {
        //
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Details(int id)
        {
            OrderViewModel orderVM = new OrderViewModel()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(id),
                //get all order details filtered by order header id
                OrderDetails = _unitOfWork.OrderDetails.GetAll(filter: o => o.OrderHeaderId == id)
            };
            //return View with viewModel 
            return View(orderVM);
        }


        public IActionResult Approve(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if(orderFromDb == null)
            {
                return NotFound();
            }
            //passing OrderHeader Id and status approved string to method
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, StaticDetails.StatusApproved);
            //changes are saved in the OrderHeaderRepository method already
            //return to Index action/view
            return View(nameof(Index));
        }

        public IActionResult Reject(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            //passing OrderHeader Id and status rejected string to method
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, StaticDetails.StatusRejected);
            //changes are saved in the OrderHeaderRepository method already
            //return to Index action/view
            return View(nameof(Index));
        }



        #region API Calls

        public IActionResult GetAllOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll() });
        }

        public IActionResult GetAllPendingOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == StaticDetails.StatusSubmitted) });
        }

        public IActionResult GetAllApprovedOrders()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == StaticDetails.StatusApproved) });
        }

        #endregion

    }
}