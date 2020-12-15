using System;
using System.ComponentModel.DataAnnotations;

namespace JWT_Midelware
{
    public class ModeloLogin
    {
        public class AuthenticateRequest
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }
    }
}
