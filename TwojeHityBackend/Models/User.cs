using System.Data;

namespace TwojeHity.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Favorite> Favorites { get; set; }
    }
}
