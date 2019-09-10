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
    public class EditModel : WF_BasePageModel
    {
        public EditModel(
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
                                                      ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string OwnerID)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            /* Fetch Contact from DB to get OwnerID.
                var WFUser = await Context
                .WudFilmAppUser.AsNoTracking()
                .FirstOrDefaultAsync(m => m.OwnerID.Equals(OwnerID));

            if (WFUser == null)
            {
                return NotFound();
            }

            var isAuthorized = await IAuthorizationService.AuthorizeAsync(
                                                     User, WFUser,
                                                     ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }*/

            WFUser.OwnerID = WFUser.OwnerID;

            Context.Attach(WFUser).State = EntityState.Modified;

            if (WFUser.Status == ContactStatus.Approved)
            {
                // If the contact is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        WFUser,
                                        ContactOperations.Approve);

                if (!canApprove.Succeeded)
                {
                    WFUser.Status = ContactStatus.Submitted;
                }
            }

            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool ContactExists(string NetID)
        {
            return Context.WudFilmAppUser.Any(e => e.NetID.Equals(NetID));
        }
    }
}

