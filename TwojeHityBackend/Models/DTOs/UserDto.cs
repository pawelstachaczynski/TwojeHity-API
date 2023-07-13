using System.Data;

namespace TwojeHity.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

    }
}
