using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Entities
{
    [Table("psicologa", Schema = "consultapsi")]
    public class Psicologa : BaseEntidade
    {
        [Column("dt_nascimento", TypeName = "Date")]
        public DateTime DtNascimento { get; set; }

        [Column("crp", TypeName = "varchar(20)")]
        public string CRP { get; set; }

        public Acesso Acesso { get; set; }

        [Column("acesso_id", TypeName = "uuid")]
        public Guid Acesso_Id { get; set; }

        public Consulta Consulta { get; set; }

    }
}
