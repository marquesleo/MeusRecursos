using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Command
{
    public class DeleteSenhaCommand : IRequest<SenhaResponse>
    {
        public Guid Id { get; set; }
    }
}
