using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Consulta.ViewModels
{
    public class EmpresaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [MaxLength(200, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [MaxLength(14, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Celular { get; set; }

         [MaxLength(14, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [MaxLength(18, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Cnpj { get; set; }

        [MaxLength(200, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Endereco { get; set; }
        [MaxLength(20, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Cep { get; set; }
        [MaxLength(150, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Cidade { get; set; }
        [MaxLength(150, ErrorMessage = "O Campo {0} precisa ter no máximo {1}")]
        public string Bairro { get; set; }
       
        public Guid Empresa_ID { get; set; }

    }
}
