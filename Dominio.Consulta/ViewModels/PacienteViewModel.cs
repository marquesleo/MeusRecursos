using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.ViewModels
{
    public class PacienteViewModel
    {
        public Guid  Id { get; set; }

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
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public DateTime DtNascimento { get; set; }
        public bool Ativo { get; set; }
        public Guid Acesso_Id { get; set; }
    }
}
