using Domain.Prioridades.ViewModels;
using System.Collections.Generic;

namespace ApplicationPrioridadesAPP.OpenApp.Senha
{
	public class SenhaResponse : Response
	{
        public SenhaViewModel Data { get; set; }
        public List<SenhaViewModel> Lista { get; set; }
    }
}

