using ApplicationPrioridadesAPP.Interfaces.Generics;
using ApplicationPrioridadesAPP.Interfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Prioridades.Interface;
using System.Linq;
using System.Security.Cryptography;
using Domain.Prioridades.ViewModels;

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppUsuario : InterfaceUsuarioApp
    {
        private readonly IUsuario _IUsuario; 
        public AppUsuario(IUsuario IUsuario)
        {
            this._IUsuario = IUsuario;
        }

        public async Task AddUsuario(LoginViewModel loginViewModel)
        {

            var usuario = new Domain.Prioridades.Entities.Usuario();
            usuario.Username = loginViewModel.Username;
            usuario.Password = Utils.Criptografia.CriptografarSenha(loginViewModel.Password);
                               
            await _IUsuario.Add(usuario);
            loginViewModel.Id = usuario.Id;
        }

        public async Task Delete(Domain.Prioridades.Entities.Usuario objeto)
        {
           await _IUsuario.Delete(objeto);
        }

        public async Task<List<Domain.Prioridades.Entities.Usuario>> FindByCondition(Expression<Func<Domain.Prioridades.Entities.Usuario, bool>> expression)
        {
            return await _IUsuario.FindByCondition(expression);
        }

        public async Task<Domain.Prioridades.Entities.Usuario> GetEntityById(Guid id)
        {
            return await _IUsuario.GetEntityById(id);
        }

        public async Task<List<Domain.Prioridades.Entities.Usuario>> List()
        {
            return await _IUsuario.List();
        }

        public async Task<Domain.Prioridades.Entities.Usuario> ObterUsuario(string login, string senha)
        {
           

            return await _IUsuario.ObterUsuario(login, senha);
        }

        public async Task UpdateUsuario(Domain.Prioridades.Entities.Usuario usuario)
        {
            await _IUsuario.Update(usuario);
        }
    }
}
