using Domain.Consulta.Entities;
using FluentValidation;


namespace Domain.Consulta.Validations
{
    public class PsicologaValidation : AbstractValidator<Psicologa>
    {
        public PsicologaValidation()
        {
            RuleFor(c => c.Nome)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
               .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Celular)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado")
              .Length(14).WithMessage("O campo {PropertyName} deve ter {MaxLength} caracteres");

             
        }
    }
}
