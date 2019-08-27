namespace MangaAPI.Models
{
    public class Rating
    {
        public Rating(int value, int mangaId, int userId)
        {
            Value = value;
            MangaId = mangaId;
            UserId = userId;
        }

        public int Id { get; set; }
        public int Value { get; set; }
        public int MangaId { get; set; }
        public int UserId { get; set; }
    }
}
