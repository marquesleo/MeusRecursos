using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Entities
{
    [Table("consulta", Schema = "consultapsi")]
    public class Consulta : Base
    {

        [Column("horario")]
        public DateTime Horario { get; set; }

        [Column("proposito", TypeName = "varchar(50)")]
        public string proposito { get; set; }

        [Column("umahora", TypeName = "boolean")]
        public bool UmaHora { get; set; }

        [Column("observacao", TypeName = "varchar(500)")]
        public string Obervacao { get; set; }

        [Column("status", TypeName = "char(1)")]
        public string Status { get; set; }

        [Column("satisfacao", TypeName = "int")]
        public int Satisfacao { get; set; }

        [Column("comentario", TypeName = "varchar(500)")]
        public string Comentario { get; set; }

        [Column("horariosatisfacao")]
        public DateTime HorarioSatisfacao { get; set; }

        [Column("javiusatisfacao", TypeName = "boolean")]
        public bool JaViuSatisfacao { get; set; }

        [Column("psicologa_id", TypeName = "uuid")]
        public Guid Psicologa_Id { get; set; }

        [Column("paciente_id", TypeName = "uuid")]
        public Guid Paciente_Id { get; set; }

        public Psicologa Psicologa { get; set; }

        public Paciente Paciente { get; set; }
    }
}
