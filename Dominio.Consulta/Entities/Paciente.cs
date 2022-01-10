using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;

namespace Domain.Consulta.Entities
{
    [Table("paciente", Schema = "consultapsi")]
    public class Paciente : BaseEntidade
    {
        [Column("dt_nascimento", TypeName = "Date")]
        public DateTime DtNascimento { get; set; }

        public virtual Acesso Acesso { get; set; }

        [Column("acesso_id", TypeName = "uuid")]
        public Guid Acesso_Id { get; set; }

        public Consulta Consulta { get; set; }

        [Column("ativo", TypeName = "boolean")]
        public bool Ativo { get; set; }
    }
}
