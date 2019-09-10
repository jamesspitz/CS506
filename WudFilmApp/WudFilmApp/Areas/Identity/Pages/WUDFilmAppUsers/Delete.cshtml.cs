using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WudFilmApp.Areas.Identity.Authorization;
using WudFilmApp.Areas.Identity.Data;
using WudFilmApp.Models;

namespace WudFilmApp.Areas.Identity.Pages.WUDFilmAppUsers
{
    public class DeleteModel : WF_BasePageModel
    {
        public DeleteModel(
           IdentityContext context,
           IAuthorizationService authorizationService,
           UserManager<IdentityUser> userManager)
           : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public WudFilmAppUser WFUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string NetID)
        {
            WFUser = await Context.WudFilmAppUser.FirstOrDefaultAsync(
                                                 m => m.NetID == NetID);

            if (WFUser == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, WFUser,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string NetID)
        {
            WFUser = await Context.WudFilmAppUser.FindAsync(NetID);

            var contact = await Context
                .WudFilmAppUser.AsNoTracking()
                .FirstOrDefaultAsync(m => m.NetID.Equals(NetID));

            if (contact == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                     User, contact,
                                                     ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            Context.WudFilmAppUser.Remove(WFUser);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

