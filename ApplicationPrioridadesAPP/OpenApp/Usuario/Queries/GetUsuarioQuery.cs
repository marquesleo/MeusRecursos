

using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Queries
{
    public class GetUsuarioQuery : IRequest<UsuarioResponse>
    {
        public Guid Id { get; set; }
    }
}
