namespace TwojeHity.Models.DTOs
{
    public class SongDto
    {
       public int Id { get; set; }
        public int rank { get; set; }
        public string title { get; set; }
        public string artist { get; set; }
        public string album { get; set; }
        public int year { get; set; }

        public FavoriteDto Favorites { get; set; }

    }
}
