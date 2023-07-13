using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TwojeHity.Models.DTOs.User
{
    public class RegisterUserDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }



    }
}