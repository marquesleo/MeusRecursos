using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Queries
{
    public class GetCategoriaQuery : IRequest<CategoriaResponse>
    {
        public Guid Id { get; set; }
        
    }
}
