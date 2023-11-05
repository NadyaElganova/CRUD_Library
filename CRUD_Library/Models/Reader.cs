namespace CRUD_Library.Models
{
    public class Reader
    {
        public int  Id { get; set; }
        public string FIO { get; set; }
        public IEnumerable<BookReader> BookReaders { get; set; }
    }
}
