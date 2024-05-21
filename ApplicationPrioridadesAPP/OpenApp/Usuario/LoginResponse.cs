
using Domain.Prioridades.Entities;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario
{
    public class LoginResponse : Response
    {
        public AuthenticateResponse Data { get; set; }
    }
}
