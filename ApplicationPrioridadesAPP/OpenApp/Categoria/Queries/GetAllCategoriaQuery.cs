using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Queries
{
    public class GetAllCategoriaQuery : IRequest<CategoriaResponse>
    {
        public Guid Id_Usuario { get; set; }
    }



}
