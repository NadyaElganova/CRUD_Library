using System.ComponentModel.DataAnnotations;

namespace CRUD_Library.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Login { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string PasswordAgain { get; set; }
    }
}
