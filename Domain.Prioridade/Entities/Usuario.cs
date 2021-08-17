using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Prioridades.Entities
{

    [Table("usuario", Schema = "personal")]
    public class Usuario : Base
    {
        [Column("username", TypeName = "varchar(50)")]
        public string Username { get; set; }

        [Column("password", TypeName = "varchar(200)")]
        [JsonIgnore]
        public string Password { get; set; }

        public void Map(ViewModels.LoginViewModel loginViewModel)
        {
            Extension.UsuarioExtension.Map(this, loginViewModel);
        }
    }
}
