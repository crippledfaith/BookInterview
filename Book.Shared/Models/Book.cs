namespace Book.Shared.Models
{
    public class Book
    {
        public long BookId { get; set; }
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string ISBN { get; set; } = default!;
    }
}