using ApplicationPrioridadesAPP.OpenApp.Prioridade;
using Domain.Prioridades.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Command
{
    public class CreateSenhaCommand : IRequest<SenhaResponse>
    {
        public SenhaViewModel SenhaViewModel { get; set; }
    }
}
