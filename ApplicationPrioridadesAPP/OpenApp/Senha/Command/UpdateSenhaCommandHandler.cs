using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Command
{
    public class UpdateSenhaCommandHandler : IRequestHandler<UpdateSenhaCommand, SenhaResponse>
    {
        public async Task<SenhaResponse> Handle(UpdateSenhaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
