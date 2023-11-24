//using CRUD_Library.Models;
//using CRUD_Library.Services;
//using System.Text.Json;

//namespace CRUD_Library.Middleware
//{
//    public class AuthMiddleware
//    {
//        private RequestDelegate next;

//        public AuthMiddleware(RequestDelegate next)
//        {
//            this.next = next;
            
//        }

//        public async Task InvokeAsync(HttpContext context, IUserManager userManager)
//        {
//            userManager.GetUserCrededantials();
//            await next.Invoke(context);
//        }
//    }
//}
