using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JWT_Midelware
{
    public class Usuario
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
