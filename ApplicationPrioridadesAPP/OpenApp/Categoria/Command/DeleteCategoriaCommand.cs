using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Command
{
    public class DeleteCategoriaCommand : IRequest<CategoriaResponse>
    {
        public Guid Id { get; set; }
    }
}
