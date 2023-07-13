using System.Data;

namespace TwojeHity.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SongId { get; set; }
    }
}
