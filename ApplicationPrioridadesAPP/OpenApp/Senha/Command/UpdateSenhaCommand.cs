using Domain.Prioridades.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Command
{
    public class UpdateSenhaCommand : IRequest<SenhaResponse>
    {
        public SenhaViewModel SenhaViewModel { get; set; }

        public Guid Id { get; set; }    
    }
}
