using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Consulta.Entities
{

    [Table("usuario", Schema = "consultapsi")]
    public class Usuario : Base
    {
        [Column("username", TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Column("email", TypeName = "varchar(200)")]
        public string Email { get; set; }

        [Column("password", TypeName = "varchar(200)")]
        [JsonIgnore]
        public string Password { get; set; }

        [Column("ativo", TypeName = "boolean")]
        public bool Ativo { get; set; }

        [JsonIgnore]
        public Acesso Acesso { get; set; }

        public void Map(ViewModels.LoginViewModel loginViewModel)
        {
            Extension.UsuarioExtension.Map(this, loginViewModel);
        }
    }
}
