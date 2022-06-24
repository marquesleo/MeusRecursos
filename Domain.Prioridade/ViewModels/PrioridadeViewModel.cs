using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Prioridades.ViewModels
{
    public class PrioridadeViewModel
    {
        [Key]
        public Guid Id { get; set; }
      
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
      
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        public int Valor { get; set; }
        public bool Ativo { get; set; }
        public bool Feito {get;set;}
        public string  Usuario { get; set; }
        
    }
}
