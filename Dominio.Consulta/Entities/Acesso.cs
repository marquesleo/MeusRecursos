using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Entities
{
    [Table("acesso", Schema = "consultapsi")]
    public class Acesso:Base
    {
        [Column("empresa_id", TypeName = "uuid")]
        public Guid Empresa_Id { get; set; }
        public Empresa Empresa { get; set; }

        [Column("usuario_id", TypeName = "uuid")]
        public Guid Usuario_Id { get; set; }
       
        public Usuario Usuario { get; set; }

        public Psicologa Psicologa { get; set; }

        public Paciente Paciente { get; set; }
        
        [Column("tipo", TypeName = "char(1)")]
        public string tipo { get; set; }
    }
}
