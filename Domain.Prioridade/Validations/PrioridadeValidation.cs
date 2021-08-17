using FluentValidation;
using Domain.Prioridades.Entities;

namespace Domain.Prioridades.Validations
{
    public class PrioridadeValidation : AbstractValidator<Prioridade>
    {
        public PrioridadeValidation()
        {
            RuleFor(c => c.Descricao)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
               .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
