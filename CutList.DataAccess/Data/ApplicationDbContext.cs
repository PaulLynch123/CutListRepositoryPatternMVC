using System;
using System.Collections.Generic;
using System.Text;
using CutList.Models;
//added extension to ClassLibrary
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//edit namespace to current Project
namespace CutList.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //put the model in the DbSet to use in accessing database
        public DbSet<Job> Job { get; set; }
    }
}
