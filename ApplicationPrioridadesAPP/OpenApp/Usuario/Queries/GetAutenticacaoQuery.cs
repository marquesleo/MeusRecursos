using Domain.Prioridades.ViewModels;
using MediatR;


namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Queries
{
    public class GetAutenticacaoQuery : IRequest<LoginResponse>
    {
        public LoginViewModel Login { get; set; }
    }
}
