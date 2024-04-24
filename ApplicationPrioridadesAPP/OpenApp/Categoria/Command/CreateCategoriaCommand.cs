

using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Command
{
    public class CreateCategoriaCommand : IRequest<CategoriaResponse>
    {
        public CategoriaViewModel CategoriaViewModel { get; set; }
    }
}
