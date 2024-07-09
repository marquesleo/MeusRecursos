using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha.Queries
{
    public class GetContadorDeSenhaQuery : IRequest<ContadorSenhaResponse>
    {
        public Guid IdSenha {  get; set; }  
    }
}
