using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WudFilmApp.Areas.Identity.Data;
using WudFilmApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WudFilmApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RoleListModel> model = new List<RoleListModel>();
            model = roleManager.Roles.Select(r => new RoleListModel
            {
                RoleName = r.Name,
                Id = r.Id,
            }).ToList();
            return View(model);
        }

        // get and post for ADD & EDIT role
        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                IdentityRole applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                }
            }
            return PartialView("_AddEditApplicationRole", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);
                IdentityRole applicationRole = isExist ? await roleManager.FindByIdAsync(id) :
               new IdentityRole
               {
               };
                applicationRole.Name = model.RoleName;
                IdentityResult roleRuslt = isExist ? await roleManager.UpdateAsync(applicationRole)
                                                    : await roleManager.CreateAsync(applicationRole);
                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // get and post for DELETE Role
        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                IdentityRole applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    name = applicationRole.Name;
                }
            }
            return PartialView("_DeleteApplicationRole", name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApplicationRole(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                IdentityRole applicationRole = await roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    IdentityResult roleRuslt = roleManager.DeleteAsync(applicationRole).Result;
                    if (roleRuslt.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
    }
}