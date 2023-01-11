using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Prioridades.Entities.Extension
{
    public static class UsuarioExtension
    {
        public static void Map(this Usuario dbUsuario, 
                               ViewModels.LoginViewModel loginViewModel)
        {
            dbUsuario.Username = loginViewModel.Username.ToUpper();
            dbUsuario.Email = loginViewModel.Email;
        }
    }
}
