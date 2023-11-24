using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CRUD_Library.Models;
using CRUD_Library.ViewModels;
using System.Text.Json;
using Registration.Services;

namespace CRUD_Library.Services
{
    public class UserManager : IUserManager
    {


        private readonly AppDbContext _userDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserManager(AppDbContext userDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _userDbContext = userDbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public UserCrededantials CurrentUser { get; set; }

        public UserCrededantials GetUserCrededantials()
        {

            try
            {
                if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
                {
                    var hash = httpContextAccessor.HttpContext.Request.Cookies["auth"];

                    var json = AesOperation.DecryptString("b14ca5898a4e4133bbce2ea2315a1916", hash);
                    CurrentUser = JsonSerializer.Deserialize<UserCrededantials>(json);
                    if (CurrentUser.Expiration >= DateTime.Now)
                    {
                        Console.WriteLine("YES USER " + CurrentUser.Expiration.ToString());
                        Console.WriteLine($"{CurrentUser.Login} = Login from GetUserCrededantials");
                        return CurrentUser;

                    }
                    else
                    {
                        CurrentUser = null;

                    }
                }
            }
            catch (Exception ex)
            {

            }
            Console.WriteLine("NO USER");
            return null;

        }

        public bool Login(string userName, string password)
        {
            var passwordHash = SHA256Encripter.Encript(password);

            var user = _userDbContext.Users.FirstOrDefault(u => u.Login == userName && u.PasswordHash == passwordHash);
            if (user != null)
            {
                //  HttpContext.Response.Cookies.Append("auth", loginView.Login);
                UserCrededantials userCrededantials = new UserCrededantials()
                {
                    Login = user.Login,
                    IsAdmin = user.IsAdmin,
                    Expiration = DateTime.Now + TimeSpan.FromMinutes(1)
                };
                var userCred = JsonSerializer.Serialize(userCrededantials);
                var hash = AesOperation.EncryptString("b14ca5898a4e4133bbce2ea2315a1916", userCred);// шифруем куки
                httpContextAccessor.HttpContext.Response.Cookies.Append("auth", hash);
                return true;

            }
            return false;
        }
    }
}
