using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutListRepositoryPatternMVC.Areas.Admin.Controllers
{
    //Autorized to only the Admin role
    [Authorize(Roles = StaticDetails.Admin)]
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            //get the current user identity to use in linq query below
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //get all user except logged in Administrator user
            return View(_unitOfWork.User.GetAll(u => u.Id != claims.Value));
        }

        public IActionResult Lock(string id)
        {
            if(id == null)
            {
                //MAKE IT GO SOMEWHERE
                return NotFound();
            }

            _unitOfWork.User.LockUser(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UnLock(string id)
        {
            if (id == null)
            {
                //MAKE IT GO SOMEWHERE
                return NotFound();
            }

            _unitOfWork.User.UnLockUser(id);
            return RedirectToAction(nameof(Index));
        }

    }
}