using CRUD_Library.Models;
using CRUD_Library.Services;
using CRUD_Library.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CRUD_Library.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IUserManager userManager;

        public AuthController(AppDbContext dbContext, IUserManager userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public IActionResult Register()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                var roles = (ClaimsIdentity)claimUser.Identity;
                var roleClaim = roles.FindFirst(ClaimTypes.Role).Value;
                if (roleClaim == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                    return RedirectToAction("Index", "Book");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                User user = await dbContext.Users.FirstOrDefaultAsync(u => u.Login == registerViewModel.Login);
                if (user == null)
                {
                    User newUser = new User()
                    {
                        Login = registerViewModel.Login,
                        PasswordHash = SHA256Encripter.Encript(registerViewModel.Password),
                        IsAdmin = false,
                    };

                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();

                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    ModelState.AddModelError("", "Логин уже занят!");
                }
            }
                return View(registerViewModel);
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                var roles = (ClaimsIdentity)claimUser.Identity;
                var roleClaim = roles.FindFirst(ClaimTypes.Role).Value;
                if(roleClaim=="Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else 
                    return RedirectToAction("Index", "Book");
            }                
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (userManager.Login(loginViewModel.Login, loginViewModel.Password))
            {
                User user = await dbContext.Users.FirstOrDefaultAsync(u => u.Login == loginViewModel.Login);
                await Authenticate(user);
                if(user.IsAdmin) return RedirectToAction("Index", "Home", new { area = "Admin" });
                else return RedirectToAction("Index", "Book");
            }

            ModelState.AddModelError("all", "Данные введены неверно!");
            return View(loginViewModel);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.IsAdmin?"Admin":"User")
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}
