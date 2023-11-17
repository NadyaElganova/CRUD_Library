using CRUD_Library.Models;

namespace CRUD_Library.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set;}
        public IEnumerable<Reader> Readers { get; set; }
        public IEnumerable<Book> RecentBook { get; set; }
        public int CurrentPages { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedReaderId { get; set; }
        public int TotalPages { get; set; }
        public int LimitPage { get; set; } = 2;

    }
}
