using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
    public class DetailsModel : WF_BasePageModel
    {
        public DetailsModel(
            IdentityContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
            : base(context, authorizationService, userManager)
        {
        }

        public WudFilmAppUser WFUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string NetID)
        {
            WFUser = await Context.WudFilmAppUser.FirstOrDefaultAsync(m => m.NetID.Equals(NetID));

            if (WFUser == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.ContactManagersRole) ||
                               User.IsInRole(Constants.ContactAdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != WFUser.OwnerID
                && WFUser.Status != ContactStatus.Approved)
            {
                return new ChallengeResult();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string NetID, ContactStatus status)
        {
            var contact = await Context.WudFilmAppUser.FirstOrDefaultAsync(
                                                      m => m.NetID.Equals(NetID));

            if (contact == null)
            {
                return NotFound();
            }

            var contactOperation = (status == ContactStatus.Approved)
                                                       ? ContactOperations.Approve
                                                       : ContactOperations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, WFUser,
                                        contactOperation);
            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }
            contact.Status = status;
            Context.WudFilmAppUser.Update(contact);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
