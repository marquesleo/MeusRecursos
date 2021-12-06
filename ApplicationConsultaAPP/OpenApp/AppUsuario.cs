using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationConsultaAPP.Interfaces;
using Domain.Consulta.Interface;
using Domain.Consulta.ViewModels;
using Domain.Consulta.Entities;

namespace ApplicationConsultaAPP.OpenApp
{
    public class AppUsuario : InterfaceUsuarioApp
    {
        private readonly IUsuario _IUsuario; 
        public AppUsuario(IUsuario IUsuario)
        {
            this._IUsuario = IUsuario;
        }

        public async Task Incluir(LoginViewModel loginViewModel)
        {

            var usuario = new Usuario();
            usuario.Username = loginViewModel.Username;
            usuario.Password = Utils.Criptografia.CriptografarSenha(loginViewModel.Password);
                               
            await _IUsuario.Add(usuario);
            loginViewModel.Id = usuario.Id;
        }

        public async Task Delete(Usuario objeto)
        {
           await _IUsuario.Delete(objeto);
        }

        public async Task<List<Usuario>> FindByCondition(Expression<Func<Usuario, bool>> expression)
        {
            return await _IUsuario.FindByCondition(expression);
        }

        public async Task<Usuario> GetEntityById(Guid id)
        {
            return await _IUsuario.GetEntityById(id);
        }

        public bool IsUsuarioExiste(string login)
        {
            var usuario = this.ObterUsuario(login).Result;
            if (usuario == null )
               return true;
            else
               return false;
        }

        public async Task<List<Usuario>> List()
        {
            return await _IUsuario.List();
        }

        public async Task<Usuario> ObterUsuario(string login, string senha)
        {
            return await _IUsuario.ObterUsuario(login, senha);
        }

        public async Task<Usuario> ObterUsuario(string login)
        {
            return await _IUsuario.ObterUsuario(login);
        }

        public async Task Alterar(Usuario usuario)
        {
            await _IUsuario.Update(usuario);
        }
    }
}
