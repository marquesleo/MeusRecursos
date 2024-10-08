using Domain.Prioridades.Validations;
using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Prioridades.Entities
{
     [Table("senha", Schema = "personal")]
     public class Senha:Base{
            
        [Column("descricao",TypeName ="varchar(200)")]
        public string Descricao { get; set; }

        public override bool Invalid { get { return !Validate(this, new SenhaValidation()); } }

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

        [Column("imagem",TypeName ="bytea")]
        public byte[] Imagem { get; set; }

        [Column("nomeimagem",TypeName ="varchar(200)")]
        public string NomeImagem { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        [JsonIgnore]
        public virtual Categoria? Categoria { get; set; }

      


        [Column("usuario", TypeName = "uuid")]
        public Guid Usuario_Id { get; set; }

        [Column("usuario_site", TypeName = "varchar(200)")]
        public string Usuario_Site { get; set; }

        [Column("categoria", TypeName = "uuid")]
        public Guid? Categoria_Id { get; set; }


        [JsonIgnore]
        public virtual ContadorDeSenha ContadorDeSenha { get; set; }


        public void Map(ViewModels.SenhaViewModel senhaViewModel)
        {
           Extension.SenhaExtension.Map(this, senhaViewModel);
        } 

     }
}