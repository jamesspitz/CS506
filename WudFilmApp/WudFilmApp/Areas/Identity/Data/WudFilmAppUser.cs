using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WudFilmApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the WudFilmAppUser class
    public class WudFilmAppUser : IdentityUser
    {
        [Required]
        public string NetID { get; set; }

        // Role of Account
        public ContactStatus Status { get; set; }

        public string OwnerID { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
