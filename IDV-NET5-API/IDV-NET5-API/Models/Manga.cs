namespace MangaAPI.Models
{
    public class Manga
    {
        public Manga( string description, string title, int year, double price, string genre)
        {
            Description = description;
            Title = title;
            Year = year;
            Price = price;
            Genre = genre;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
    }
}
