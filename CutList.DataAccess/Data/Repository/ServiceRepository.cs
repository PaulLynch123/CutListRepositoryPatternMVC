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
    public class ServiceRepository : Repository<Service>, IServiceRepository        //MAKE NOTES ON THIS
    {
        //need database object
        private readonly ApplicationDbContext _db;

        //constructor to retrieve the database object
        public ServiceRepository(ApplicationDbContext db) : base(db)        //exspecting parameter in constructor can now retrieve from implementing base(db)
        {
            _db = db;
        }

        public void Update(Service service)
        {
            var objectFromDb = _db.Service.FirstOrDefault(j => j.Id == service.Id);

            objectFromDb.Name = service.Name;
            objectFromDb.Price = service.Price;
            objectFromDb.LongDescription = service.LongDescription;
            objectFromDb.ImageUrl = service.ImageUrl;
            objectFromDb.JobId = service.JobId;
            objectFromDb.FrequencyId = service.FrequencyId;


            _db.SaveChanges();

        }
    }
}
