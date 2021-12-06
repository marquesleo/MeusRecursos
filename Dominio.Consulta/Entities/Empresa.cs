using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;

namespace Domain.Consulta.Entities
{
    [Table("empresa", Schema = "consultapsi")]
    public class Empresa:BaseEntidade
    {
        [Column("cnpj", TypeName = "varchar(18)")]
        public string cnpj { get; set; }

        public Acesso Acesso { get; set; }

    }
}
