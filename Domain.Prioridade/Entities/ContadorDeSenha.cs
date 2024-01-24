using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Entities.Models;

namespace Domain.Prioridades.Entities
{
    [Table("contadorsenha", Schema = "personal")]
    public class ContadorDeSenha : Base
    {
		public ContadorDeSenha()
		{
		}
        [JsonIgnore]
        public virtual Senha Senha { get; set; }

        [Column("senha", TypeName = "uuid")]
        public Guid SenhaId { get; set; }
        [Column("contador")]
        public int Contador { get; set; }
        [Column("DataDeAcesso")]
        public DateTime DataDeAcesso { get; set; }
	}
}

