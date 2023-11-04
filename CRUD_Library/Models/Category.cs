using System.ComponentModel.DataAnnotations;

namespace CRUD_Library.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        public ICollection<Book>? Books { get; set;}
    }
}
