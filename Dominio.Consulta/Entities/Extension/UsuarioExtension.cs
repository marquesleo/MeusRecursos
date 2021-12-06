

namespace Domain.Consulta.Entities.Extension
{
    public static class UsuarioExtension
    {
        public static void Map(this Usuario dbUsuario, 
                               ViewModels.LoginViewModel loginViewModel)
        {
            dbUsuario.Username = loginViewModel.Username;
            //dbUsuario.Password = loginViewModel.Password;
        }
    }
}
