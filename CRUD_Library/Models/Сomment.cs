using System.ComponentModel.DataAnnotations;

namespace CRUD_Library.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage ="Where is comment?")]
        [MaxLength(30)]
        public string? Text { get; set; }
        public DateTime Date { get; set; }
        public Book? Book { get; set; }
    }
}
