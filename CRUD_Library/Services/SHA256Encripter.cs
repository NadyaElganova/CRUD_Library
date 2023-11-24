using System.Security.Cryptography;
using System.Text;

namespace CRUD_Library.Services
{
    public static class SHA256Encripter
    {

        public static string Encript(string str)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
        
    }
}
