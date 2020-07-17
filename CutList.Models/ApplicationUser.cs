using Microsoft.AspNetCore.Identity;                //extended from Identity Scaffolding table
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CutList.Models
{
    //Identity user is ASP.NET user table
    public class ApplicationUser : IdentityUser
    {
        //Put in an additional fields that I want to have in my application
        [Required]
        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }

    }
}
