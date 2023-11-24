using CRUD_Library.Models;
using CRUD_Library.Services;
using CRUD_Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
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
                return View(registerViewModel);
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (userManager.Login(loginViewModel.Login, loginViewModel.Password))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("all", "Данные введены неверно!");
            return View(loginViewModel);
        }
    }
}
