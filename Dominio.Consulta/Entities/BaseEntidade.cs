using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Entities
{
    public class BaseEntidade:Base
    {
        [Column("nome", TypeName = "varchar(200)")]
        public string Nome { get; set; }

        [Column("email", TypeName = "varchar(200)")]
        public string Email { get; set; }

        [Column("celular", TypeName = "varchar(14)")]
        public string Celular { get; set; }

        [Column("telefone", TypeName = "varchar(14)")]
        public string Telefone { get; set; }

        [Column("endereco", TypeName = "varchar(200)")]
        public string Endereco { get; set; }

        [Column("cep", TypeName = "varchar(20)")]
        public string Cep { get; set; }

        [Column("cidade", TypeName = "varchar(150)")]
        public string Cidade { get; set; }

        [Column("bairro", TypeName = "varchar(150)")]
        public string Bairro { get; set; }

    }
}
