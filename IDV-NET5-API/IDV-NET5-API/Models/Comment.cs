namespace MangaAPI.Models
{
    public class Comment
    {
        public Comment(int mangaId, int userId, string text)
        {
            MangaId = mangaId;
            UserId = userId;
            Text = text;
        }

        public int Id {get; set;}
        public int MangaId { get; set; }
        public int UserId { get; set; } 
        public string Text { get; set; }
        
    }
}
