using Domain.Prioridades.ViewModels;
using System.Collections.Generic;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade
{
	public class PrioridadeResponse : Response
	{
        public PrioridadeViewModel Data { get; set; }
        public List<PrioridadeViewModel> Lista { get; set; }
    }
}

