using Domain.Consulta.Entities;
using FluentValidation;
namespace Domain.Consulta.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(c => c.Username)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
               .Length(2, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


        }
    }
}
