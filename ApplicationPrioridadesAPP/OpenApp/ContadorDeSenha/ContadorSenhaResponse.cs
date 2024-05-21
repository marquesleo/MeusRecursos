
using Domain.Prioridades.ViewModels;
using System.Collections.Generic;

namespace ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha
{
    public class ContadorSenhaResponse : Response
    {

        public ContadorSenhaViewModel Data { get; set; }
        public List<ContadorSenhaViewModel> List {  get; set; } 

    }
}
