using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Prioridades.Entities
{

    public enum enuOrdem
    {
        up,
        down
    }

    [Table("prioridade", Schema = "personal")]
    public class Prioridade:Base
    {
        [Column("descricao",TypeName ="varchar(200)")]
        
        public string Descricao { get; set; }
        [Column("valor")]

        public int Valor { get; set; }
        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("feito")]
        public bool Feito { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        [Column("usuario", TypeName = "uuid")]
        public Guid Usuario_Id { get; set; }

        public void Map(ViewModels.PrioridadeViewModel prioridadeViewModel)
        {
            Extension.PrioridadeExtension.Map(this, prioridadeViewModel);
        }

    }
}
