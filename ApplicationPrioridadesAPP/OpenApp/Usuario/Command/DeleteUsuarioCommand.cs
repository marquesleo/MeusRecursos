using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using MediatR;
using System;


namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Command
{
    public class DeleteUsuarioCommand  :IRequest<UsuarioResponse>
    {
        public Guid Id { get; set; }
    }
}
