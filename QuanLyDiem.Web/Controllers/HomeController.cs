using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuanLyDiem.Web.Models;
using QuanLyDiem.Web.ViewModels;

namespace QuanLyDiem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(string returnUrl)
        {
            ClaimsPrincipal currentUser = this.User;
            if (currentUser != null)
            {
                if (currentUser.IsInRole("Administrator"))
                {
                    return RedirectToAction("Index", "StudentManager");
                }else if (currentUser.IsInRole("Lecturer"))
                {
                    return RedirectToAction("LecturerGMIndex", "GradesManager");
                }
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password,false,false);
                if (result.Succeeded)
                {
                    IEnumerable<String> roles = await _userManager.GetRolesAsync(user);
                    switch (roles.FirstOrDefault())
                    {
                        case "Student":
                            return RedirectToAction("StudentGMIndex", "GradesManager");
                        case "Lecturer":
                            return RedirectToAction("LecturerGMIndex", "GradesManager");
                        case "Administrator":
                            return RedirectToAction("Index", "StudentManager");
                        default:
                            return View(loginViewModel);
                    }
                }
            }
            
            ModelState.AddModelError("", "Username/password not found");
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}