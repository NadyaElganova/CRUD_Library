using CRUD_Library.Models;

namespace CRUD_Library.Services
{
    public interface IUserManager
    {
        bool Login(string userName, string password);

        UserCrededantials GetUserCrededantials();
        UserCrededantials CurrentUser { get; set; }
    }
}
