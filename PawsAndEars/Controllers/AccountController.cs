﻿using System;
using System.Data.Entity.SqlServer.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PawsAndEars.EF.Entities;
using PawsAndEars.Models;
using PawsAndEars.Services;

namespace PawsAndEars.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                User user = userManager.Find(login.Email, login.Password);
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created. 
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(login);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            int startHours = Convert.ToInt32(model.StartTime.Split(':')[0]);
            int endHours = Convert.ToInt32(model.EndTime.Split(':')[0]);
            if (endHours - startHours <= 0)
            {
                ModelState.AddModelError("", "Время конца рабочего дня должно быть больше времени начала");
                return View(model);
            }
            else
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;
                if (ModelState.IsValid)
                {
                    var user = new User { UserName = model.Email, Email = model.Email, StartWorkingTime = DateTime.Today.AddHours(startHours), EndWorkingTime = DateTime.Today.AddHours(endHours) };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        ClaimsIdentity claimsIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).WithCurrentCulture();
                        authManager.SignIn(
                            new AuthenticationProperties { IsPersistent = false }, claimsIdentity);

                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}