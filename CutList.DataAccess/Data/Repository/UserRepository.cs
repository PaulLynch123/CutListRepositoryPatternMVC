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
    public class UserRepository : Repository<ApplicationUser>, IUserRepository        //MAKE NOTES ON THIS
    {
        //need database object
        private readonly ApplicationDbContext _db;

        //constructor to retrieve the database object
        public UserRepository(ApplicationDbContext db) : base(db)        //exspecting parameter in constructor can now retrieve from implementing base(db)
        {
            _db = db;
        }

        //implement Interface
        public void LockUser(string userId)
        {
            //get user from Db
            var userFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            //use the Lockoutend property to effectively revoke user... lock user for 1000years
            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            _db.SaveChanges();
        }

        public void UnLockUser(string userId)
        {
            //get user from Db
            var userFromDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            //use the Lockoutend property and set to now
            userFromDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }

    }
}
