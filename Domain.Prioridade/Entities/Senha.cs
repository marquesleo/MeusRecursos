using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Prioridades.Entities
{
     [Table("senha", Schema = "personal")]
     public class Senha:Base{
            
        [Column("descricao",TypeName ="varchar(200)")]
        public string Descricao { get; set; }


        [Column("observacao",TypeName ="text")]
        public string Observacao { get; set; }

        [Column("site",TypeName ="varchar(500)")]
        public string Site { get; set; }

        [Column("ativo")]
        public bool Ativo { get; set; }

        [Column("password",TypeName ="varchar(500)")]
        public string Password { get; set; }

        [Column("data_atualizacao")]
        public DateTime DtAtualizacao { get; set; }

        [Column("url_img_site",TypeName ="text")]
        public string UrlImageSite { get; set; }

        [ForeignKey("usuario")]
        public virtual Usuario Usuario { get; set; }
        public void Map(ViewModels.SenhaViewModel senhaViewModel)
        {
           Extension.SenhaExtension.Map(this, senhaViewModel);
        } 

     }
}