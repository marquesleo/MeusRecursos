using Entities.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Prioridades.Entities
{
    [Table("categoria", Schema = "personal")]
    public class Categoria : Base
    
	{
		public Categoria()
		{
		}

        [Column("descricao", TypeName = "varchar(200)")]
        public string Descricao { get; set; }
       
        
        [Column("ativo")]
        public bool Ativo { get; set; }

        [JsonIgnore]  
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

