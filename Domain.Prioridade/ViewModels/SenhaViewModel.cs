using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Prioridades.ViewModels
{
    public class SenhaViewModel
    {
        [Key]
        public Guid Id { get; set; }
      
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public string Site { get; set; }
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
         public string Password { get; set; }

         [JsonIgnore]
         public DateTime DtAtualizacao { get; set; }
         public string UrlImageSite { get; set; }
         public string  Usuario { get; set; }
         
         public string  Usuario_Site { get; set; }
         public string Observacao {get;set;}
         
         public string ImagemData {get;set;}
         public string NomeDaImagem {get;set;}        
    }
}
