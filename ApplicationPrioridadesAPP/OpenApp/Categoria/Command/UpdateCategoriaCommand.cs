using Domain.Prioridades.ViewModels;
using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Command
{
    public class UpdateCategoriaCommand: IRequest<CategoriaResponse>
    {
        public CategoriaViewModel CategoriaViewModel { get; set; }
        public Guid Id { get; set; }

    }
}
