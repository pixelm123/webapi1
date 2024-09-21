using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        public string? Username  {get; set;}

        [Required]
        [EmailAddress]
        public string EmailAdress {get; set;}

        [Required]
        public string? Password {get; set;}

    }
}