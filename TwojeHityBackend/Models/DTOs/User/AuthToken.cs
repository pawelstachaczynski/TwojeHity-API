using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwojeHity.Models.DTOs.User
{
    public class AuthToken
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}
