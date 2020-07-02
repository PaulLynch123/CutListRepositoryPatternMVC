using CutList.DataAccess.Data;
using CutList.DataAccess.Data.Repository.IRepository;
using CutList.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CutList.DataAccess.Data.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository        //MAKE NOTES ON THIS
    {
        //need database object
        private readonly ApplicationDbContext _db;

        //constructor to retrieve the database object
        public JobRepository(ApplicationDbContext db) : base(db)        //exspecting parameter in constructor can now retrieve from implementing base(db)
        {
            _db = db;
        }

        //to populate my dropdown
        public IEnumerable<SelectListItem> GetJobListForDropDown()
        {
            return _db.Job.Select(j => new SelectListItem()
            {
                Text = j.Won.ToString() + " - " + j.Project_Name,
                Value = j.Id.ToString()
            });
        }

        public void Update(Job job)
        {
            var objectFromDb = _db.Job.FirstOrDefault(j => j.Id == job.Id);

            objectFromDb.Won = job.Won;
            //keep record of old shipping date (previousShipping date???)
            if(objectFromDb.Rqd_Ship_Date != job.Rqd_Ship_Date)
            {
                //var tempShipDate = objectFromDb.Rqd_Ship_Date;
                objectFromDb.Orig_Rqd_Ship_Date = objectFromDb.Rqd_Ship_Date;
            }
            objectFromDb.Rqd_Ship_Date = job.Rqd_Ship_Date;

            _db.SaveChanges();

        }
    }
}
