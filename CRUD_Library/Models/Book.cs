using System.ComponentModel.DataAnnotations;

namespace CRUD_Library.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Where is Title?")]
        [MaxLength(50)]
        //[DataType(DataType.Password)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Where is Description?")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Where is ImageUrl?")]
        [Display(Name ="Photo URL")]
        [DataType(DataType.Upload)]
        public string ImageUrl { get; set; }

        public Category Category { get; set; }
        public IEnumerable<BookReader> BookReaders { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
