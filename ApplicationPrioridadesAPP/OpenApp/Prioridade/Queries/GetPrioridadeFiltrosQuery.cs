using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries
{
    public class GetPrioridadeFiltrosQuery : IRequest<SenhaResponse>
    {
       public string Usuario_Id { get; set; } 
       public bool? Feito { get; set; }
    }
}
