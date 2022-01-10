using FluentValidation;

namespace Domain.Consulta.Validations
{
    public class ConsultaValidation : AbstractValidator<Entities.Consulta>
    {

        public ConsultaValidation()
        {
            RuleFor(c => c.Horario)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado");

            RuleFor(c => c.Paciente_Id)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado");

            RuleFor(c => c.Psicologa_Id)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser informado");




        }
    }
}
