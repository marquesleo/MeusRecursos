

using Domain.Prioridades.ViewModels;
using System.Collections.Generic;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Queries
{
    public class UsuarioResponse : Response
    {

        public LoginViewModel Data { get; set; } 
        
        public List<LoginViewModel> Lista { get; set; } 
    }
}
