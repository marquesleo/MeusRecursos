using Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Prioridades.Entities
{

    [Table("usuario", Schema = "personal")]
    public class Usuario : Base
    {
        
        [Column("username", TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Column("email", TypeName = "varchar(200)")]
        public string Email { get; set; }

        [Column("password", TypeName = "varchar(200)")]
        public string Password { get; set; }


        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        [JsonIgnore]
        public Senha senha { get; set; }

        [JsonIgnore]
        public Categoria categoria { get; set; }

        [JsonIgnore]
        public Prioridade prioridade { get; set; }




        public void Map(ViewModels.LoginViewModel loginViewModel)
        {
            Extension.UsuarioExtension.Map(this, loginViewModel);
        }
    }
}
