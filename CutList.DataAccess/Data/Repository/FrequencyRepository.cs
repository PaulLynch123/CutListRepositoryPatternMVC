using CutList.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using CutList.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CutList.DataAccess.Data.Repository
{
    public class FrequencyRepository : Repository<Frequency> , IFrequencyRepository     //IMPLEMENT INTERFACE
    {
        private readonly ApplicationDbContext _db;

        public FrequencyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //IMPLEMENT INTERFACE
        public IEnumerable<SelectListItem> GetFrequencyForDropdown()
        {
            return _db.Frequency.Select(f => new SelectListItem()
            {
                Text = f.Name,
                Value = f.Id.ToString()
            });
        }

        public void Update(Frequency frequency)
        {
            var objectFromDb = _db.Frequency.FirstOrDefault(f => f.Id == frequency.Id);

            objectFromDb.Name = frequency.Name;
            objectFromDb.FrequencyCount = frequency.FrequencyCount;

            _db.SaveChanges();
        }
    }
}
