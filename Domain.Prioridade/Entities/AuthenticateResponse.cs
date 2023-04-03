using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Prioridades.Entities
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string token { get; set; }
        [JsonPropertyName("refreshtoken")]
        public string RefreshToken { get; set; }

        public AuthenticateResponse(Usuario user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Username = user.Username;
            token = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
