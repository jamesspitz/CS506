using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WudFilmApp.Areas.Identity.Data;
using WudFilmApp.Models;

namespace WudFilmApp.Areas.Identity.Pages.WUDFilmAppUsers
{
    public class WF_BasePageModel : PageModel 
        {
            protected IdentityContext Context { get; }
            protected IAuthorizationService AuthorizationService { get; }
            protected UserManager<IdentityUser> UserManager { get; }

            public WF_BasePageModel(
                IdentityContext context,
                IAuthorizationService authorizationService,
                UserManager<IdentityUser> userManager) : base()
            {
                Context = context;
                UserManager = userManager;
                AuthorizationService = authorizationService;
            }
        }
}
