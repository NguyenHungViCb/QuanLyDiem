using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyDiem.Data.Models;
using QuanLyDiem.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuanLyDiem.Web.ViewComponents
{
    public class ResetPassword : ViewComponent
    {

        private readonly IStudentRepository _studentRepositories;
        private readonly UserManager<IdentityUser> _userManager;
        public async Task<IViewComponentResult> InvokeAsync(int id, string password, string newPassword)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            Student existing = null;
            IdentityUser user = null;
            if (await _userManager.IsInRoleAsync(currentUser,"Student"))
            {
                existing = _studentRepositories.GetStudentByEmail(currentUser.Email);
                user = await _userManager.FindByEmailAsync(existing.Email);
            }
            else
            {
                existing = _studentRepositories.GetStudentById(id);
                user = await _userManager.FindByEmailAsync(existing.Email);
            }
            if (user != null && existing != null)
            {
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                    _studentRepositories.ResetPassword(existing.StudentId, newPassword);
                    return View({ })
                }
                ModelState.AddModelError("", "Wrong Password");
            }
            return RedirectToAction("StudentDetails", existing.StudentId);

            return View(new ResetpasswordViewModel { })
        }
    }
}
