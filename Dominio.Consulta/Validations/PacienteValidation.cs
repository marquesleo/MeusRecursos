using Domain.Consulta.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Validations
{
    public class PacienteValidation : AbstractValidator<Paciente>
    {
        public PacienteValidation()
        {
            RuleFor(c => c.Nome)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
               .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Celular)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
              .MaximumLength(14).WithMessage("O campo {PropertyName} deve ter {MaxLength} caracteres");

             
        }
    }
}
