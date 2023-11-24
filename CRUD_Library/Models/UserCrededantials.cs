namespace CRUD_Library.Models
{
    public class UserCrededantials
    {
        public string Login { get; set; }
        public bool IsAdmin { get; set; }

        public DateTime Expiration { get; set; }
    }
}
