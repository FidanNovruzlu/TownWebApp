using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TownWebApp.Models;
using TownWebApp.ViewModels.AccountVM;

namespace TownWebApp.Controllers
{
    public class AccountController:Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
       public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid) return View(registerVM);

            AppUser appUser = new AppUser()
            {
                UserName = registerVM.UserName,
                Name = registerVM.Name,
                Email = registerVM.Email,
                Surname = registerVM.Surname
            };

            IdentityResult identityResult =    await _userManager.CreateAsync(appUser,registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach(IdentityError? error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(registerVM);
                }
            }

            IdentityResult result = await _userManager.AddToRoleAsync(appUser, "Admin");
            if (!result.Succeeded)
            {
                foreach (IdentityError? error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(registerVM);
                }
            }

            return RedirectToAction(nameof(Login));
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            AppUser appUser=   await _userManager.FindByNameAsync(loginVM.UserName);
            if (appUser == null) return NotFound();


            Microsoft.AspNetCore.Identity.SignInResult signInResult=  await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, false);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid password or username.");
            }


            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        #region Create Role 
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole()
        //    {
        //        Name="Admin"
        //    };
        //    if (role == null) return NotFound();

        //    await _roleManager.CreateAsync(role);
        //    return Json("OK");
        //}
        #endregion
    }
}
