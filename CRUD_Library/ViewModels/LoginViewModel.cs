using System.ComponentModel.DataAnnotations;

namespace CRUD_Library.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Login { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
