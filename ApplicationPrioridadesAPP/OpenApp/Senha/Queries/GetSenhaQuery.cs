

using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Queries
{
    public class GetSenhaQuery :  IRequest<SenhaResponse>
    {
        public  Guid Id {  get; set; }  
    }
}
