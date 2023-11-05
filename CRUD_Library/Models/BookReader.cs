namespace CRUD_Library.Models
{
    public class BookReader
    {
        public int Id { get; set; }
        public int BookId { get; set; } 
        public Book Book { get; set; }
        public int ReaderId { get; set; }
        public Reader Reader { get; set; }
    }
}
