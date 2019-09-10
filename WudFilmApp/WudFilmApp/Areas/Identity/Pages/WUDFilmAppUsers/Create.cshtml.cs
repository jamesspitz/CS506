using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WudFilmApp.Areas.Identity.Authorization;
using WudFilmApp.Areas.Identity.Data;
using WudFilmApp.Models;

namespace WudFilmApp.Areas.Identity.Pages.WUDFilmAppUsers
{
    public class Create
    {
        public class CreateModel : WF_BasePageModel
        {
            public CreateModel(
                IdentityContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager)
                : base(context, authorizationService, userManager)
            {
            }

            public IActionResult OnGet()
            {
                WudFilmAppUser = new WudFilmAppUser
                {
                    NetID = "WUD",
                    Email = "WUD@wisc.edu"
                    
                };
                return Page();
            }

            [BindProperty]
            public WudFilmAppUser WudFilmAppUser { get; set; }

            
            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                WudFilmAppUser.OwnerID = UserManager.GetUserId(User);

                // requires using ContactManager.Authorization;
                var isAuthorized = await AuthorizationService.AuthorizeAsync(User, WudFilmAppUser, ContactOperations.Create);
                if (!isAuthorized.Succeeded)
                {
                    return new ChallengeResult();
                }

                Context.WudFilmAppUser.Add(WudFilmAppUser);
                await Context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
        }
    }
}

