using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WudFilmApp.Areas.Identity.Data
{
    /// <summary>
    /// Class that can send emails.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //TODO: Make this send an email 
            return Task.CompletedTask;
        }
    }
}
