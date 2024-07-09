using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Queries
{
    public class GetSenhaPorUsuarioQuery: IRequest<SenhaResponse>
    {
        public Guid UsuarioId {  get; set; }

       
    }
}
