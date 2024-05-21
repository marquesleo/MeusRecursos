using FluentValidation;
using Domain.Prioridades.Entities;

namespace Domain.Prioridades.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(c => c.Username)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
               .Length(2, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


            RuleFor(c => c.Password)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
              .Length(1, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");





        }
    }
}
