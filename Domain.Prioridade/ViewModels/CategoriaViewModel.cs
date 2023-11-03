using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Prioridades.ViewModels
{
	public class CategoriaViewModel
	{

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        public Guid Usuario_Id  { get; set; }
            
        public string UrlImageSite { get; set; }
                
        public string ImagemData { get; set; }
              
        public string NomeImagem { get; set; }
    }
}

