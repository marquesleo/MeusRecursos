
using Domain.Prioridades.Entities;
using FluentValidation;

namespace Domain.Prioridades.Validations
{
    public class SenhaValidation : AbstractValidator<Senha>
    {

        public SenhaValidation()
        {
            RuleFor(c => c.Descricao)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
           .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Usuario_Site)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(1, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Password)
           .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        }
    }
}
