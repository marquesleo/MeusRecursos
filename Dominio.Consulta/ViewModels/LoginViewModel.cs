using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Consulta.ViewModels
{
    public class LoginViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(30, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Username { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [MaxLength(10, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Password { get; set; }
       
    }
}
