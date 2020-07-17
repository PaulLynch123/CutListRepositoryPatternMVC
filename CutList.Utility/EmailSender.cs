using Microsoft.AspNetCore.Identity.UI.Services;                //IEmailSender
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CutList.Utility
{
    //implement the IEmailSender inferface
    public class EmailSender : IEmailSender
    {
        //I HAVE NOT IMPLEMENTED ANYTHING FOR EMAIL BUT NEEDED TO AUTO IMPLEMENT THIS TO PREVENT REGISTRATION ERROR
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
