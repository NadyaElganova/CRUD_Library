using CRUD_Library.Models;

namespace CRUD_Library.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set;}
        public IEnumerable<Reader> Readers { get; set; }    

    }
}
