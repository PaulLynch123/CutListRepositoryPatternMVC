using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutListRepositoryPatternMVC.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class FrequencyController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        //use dependency injection to access
        public FrequencyController(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            //empty view
            return View();
        }


        // GET
        public IActionResult Upsert(int? id)
        {
            Frequency frequency = new Frequency();
            //when no id
            if (id == null)
            {
                //return empty model in view
                return View(frequency);
            }

            //using 
            frequency = _unitofwork.Frequency.Get(id.GetValueOrDefault());      
            //id doesn't exist
            if (frequency == null)
            {
                //DECIDE WHAT TO DO FOR NOT FOUND MAYBE A POPUP??? AND RETURN TO A PAGE
                return NotFound();
            }
            //return the object from database
            return View(frequency);
        }

        // POST
        // To protect from overposting attacks, choose properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Frequency frequency)
        {
            //checking against validation
            if (ModelState.IsValid)
            {
                //object in view is empty then add it to database
                if (frequency.Id == 0)
                {
                    _unitofwork.Frequency.Add(frequency);
                }
                else
                //if not the update database as per IFequencyRepository method update
                {
                    _unitofwork.Frequency.Update(frequency);
                }
                _unitofwork.Save();
                //redirect to VIew nameof Index
                return RedirectToAction(nameof(Index));
            }
            return View(frequency);

        }



        #region API Calls

        public IActionResult GetAll()
        {
            //use API call and DataTables for GetAll via _unitofwork all the way up to Repository
            return Json(new { data = _unitofwork.Frequency.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objectFromDb = _unitofwork.Frequency.Get(id);
            //if no id found
            if(objectFromDb == null)
            {
                //return Json with success set to false for popups to be able to know and display message
                return Json(new { success = false, message = "Error while deleting. 'Frequency'." });
            }
            //remove from database context and save
            _unitofwork.Frequency.Remove(objectFromDb);
            _unitofwork.Save();
            //return JSon with success set to true and the message available for popup
            return Json(new { success = true, message = "Delete was succeessful. 'Frequency'." });
        }

        #endregion
    }
}