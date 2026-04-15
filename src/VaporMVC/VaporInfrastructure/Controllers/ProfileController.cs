using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VaporDomain.Model;
using VaporInfrastructure.ViewModel;

namespace VaporInfrastructure.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            ViewBag.Username = user.UserName;
            ViewBag.Email = user.Email;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeNickname(ChangeNicknameViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
            { 
                return NotFound();
            }

            var oldNickname = user.UserName;
            user.UserName = model.NewNickname;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                ViewBag.Email = user.Email;
                ViewBag.Username = oldNickname;

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("NewNickname", error.Description);
                }
                return View("Index", model);
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
            { 
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Email = user?.Email;
                ViewBag.Username = user?.UserName;
                return View("Index", model);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index");
            }

            ViewBag.Email = user.Email;
            ViewBag.Username = user.UserName;

            foreach (var error in result.Errors)
            {
                if (error.Code == "PasswordMismatch")
                {
                    ModelState.AddModelError("OldPassword", "Неправильний поточний пароль.");
                }
                else
                {
                    ModelState.AddModelError("NewPassword", error.Description);
                }
            }

            return View("Index", model);
        }
    }
}