using Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Prioridades.Entities
{
    [Table("prioridade", Schema = "personal")]
    public class Prioridade:Base
    {
        [Column("descricao",TypeName ="varchar(200)")]
        
        public string Descricao { get; set; }
        [Column("valor")]

        public int Valor { get; set; }
        [Column("ativo")]
        public bool Ativo { get; set; }
        [ForeignKey("usuario")]
        public virtual Usuario Usuario { get; set; }
        public void Map(ViewModels.PrioridadeViewModel prioridadeViewModel)
        {
            Extension.PrioridadeExtension.Map(this, prioridadeViewModel);
        }

    }
}
