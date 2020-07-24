using CutList.Models;
using CutList.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CutList.DataAccess.Data.Initializer
{
    public class DbInitialiser : IDbInitialiser
    {
        //new role
        //new users

        private readonly ApplicationDbContext _db;
        //from Idnetity - API for managing users
        private readonly UserManager<IdentityUser> _userManager;
        //identity - API for managing roles
        private readonly RoleManager<IdentityRole> _roleManager;

        //constructor
        public DbInitialiser(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public void Initialise()
        {
            try
            {
                //if any pending migrations
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex1)
            {
                //do nothing with exception
            }
            //check if the 'Admin' role is there yet
            if (_db.Roles.Any(r => r.Name == StaticDetails.Admin)) return;
            //will make this role before doing the next statement
            _roleManager.CreateAsync(new IdentityRole(StaticDetails.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(StaticDetails.Management)).GetAwaiter().GetResult();

            //applicationUser is my class with few more properties rather than IdentityUser class
            _userManager.CreateAsync(new ApplicationUser
            {
                //assign the first users properties (not admin role yet)
                UserName = "admin@hotmail.com",
                Email = "admin@hotmail.com",
                EmailConfirmed = true,
                Name = "Paul Lynch"
                //pass the password that I want it to be for initial user login
            }, "Admin123*").GetAwaiter().GetResult();

            //get the user we just created
            ApplicationUser user = _db.ApplicationUser.Where(u => u.Email == "admin@hotmail.com").FirstOrDefault();
            //add the user we just created to the Admin role
            _userManager.AddToRoleAsync(user, StaticDetails.Admin).GetAwaiter().GetResult();
        }
    }
}
