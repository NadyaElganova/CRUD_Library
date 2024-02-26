using CRUD_Library.Models;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Library.ViewModels
{
    public class DetailViewModel
    {
        public Book Book { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "Where is comment?")]
        [MaxLength(30)]
        [MinLength(1)]
        public string? Text { get; set; }
        public DateTime Date { get; set; }
    }
}
