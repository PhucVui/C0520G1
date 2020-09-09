﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffManagment.Models;
using StaffManagment.Models.ViewModels;

namespace StaffManagment.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ExtendIdentityUser> userManager;

        public UserController(RoleManager<IdentityRole> roleManager,
                                UserManager<ExtendIdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult List()
        {
            var viewUsers = new List<ViewUser>();
            var users = userManager.Users;
            if(users != null)
            {
                foreach(var user in users)
                {
                    var vUser = new ViewUser()
                    {
                        Id = user.Id,
                        Email = user.Email
                    };
                    viewUsers.Add(vUser);
                }
            }
            if(viewUsers != null)
            {
                foreach(var user in viewUsers)
                {
                    user.RoleName = GetRoleName(user.Id);
                }
            }
            return View(viewUsers);
        }

        private string GetRoleName(string userId)
        {
            var user = Task.Run(async () => await userManager.FindByIdAsync(userId)).Result;
            var roles = Task.Run(async () => await userManager.GetRolesAsync(user)).Result;
            return (roles != null && roles.Count > 0) ? roles.FirstOrDefault() : "";
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(CreateRole model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var role = new IdentityRole()
        //        {
        //            Name = model.RoleName
        //        };

        //        var createResult = await roleManager.CreateAsync(role);
        //        if (createResult.Succeeded)
        //        {
        //            return RedirectToAction("List", "Role");
        //        }
        //        foreach(var error in createResult.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }
        //    return View(model);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    var existRole = await roleManager.FindByIdAsync(id);
        //    var roleView = new EditRole();
        //    if (existRole != null)
        //    {
        //        roleView.RoleId = existRole.Id;
        //        roleView.RoleName = existRole.Name;
        //        return View(roleView);
        //    }
        //    ModelState.AddModelError("", "Invalid role");
        //    return View(roleView);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(EditRole model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existRole = await roleManager.FindByIdAsync(model.RoleId);
        //        if(existRole == null)
        //        {
        //            ModelState.AddModelError("", "Invalid role");
        //            return View(model);
        //        }
        //        existRole.Name = model.RoleName;

        //        var updateResult = await roleManager.UpdateAsync(existRole);
        //        if (updateResult.Succeeded)
        //        {
        //            return RedirectToAction("List", "Role");
        //        }
        //        foreach (var error in updateResult.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }
        //    return View(model);
        //}

        //[Route("/Role/Delete/{roleId}")]
        //public IActionResult Delete(string roleId)
        //{
        //    var deleteResult = false;
        //    var existRole = Task.Run(async () => await roleManager.FindByIdAsync(roleId)).Result;
        //    if (existRole == null)
        //    {
        //        return Json(new { deleteResult });
        //    }
        //    var identityResult = Task.Run(async () => await roleManager.DeleteAsync(existRole)).Result;
        //    deleteResult = identityResult.Succeeded;
        //    return Json(new { deleteResult });
        //}
    }
}
