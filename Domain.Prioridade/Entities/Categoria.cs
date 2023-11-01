using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Prioridades.Entities
{
    [Table("categoria", Schema = "personal")]
    public class Categoria
	{
		public Categoria()
		{
		}

        [Column("descricao", TypeName = "varchar(200)")]
        public string Descricao { get; set; }
       
        
        [Column("ativo")]
        public bool Ativo { get; set; }

       
        [ForeignKey("usuario")]
        public virtual Usuario Usuario { get; set; }

        [Column("usuario", TypeName = "uuid")]
        public Guid Usuario_Id { get; set; }

        [Column("url_img_site", TypeName = "text")]
        public string UrlImageSite { get; set; }

        [Column("imagem", TypeName = "bytea")]
        public byte[] Imagem { get; set; }

        [Column("nomeimagem", TypeName = "varchar(200)")]
        public string NomeImagem { get; set; }

    }
}

