using ApplicationPrioridadesAPP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Prioridades.Interface;
using Domain.Prioridades.ViewModels;
using Domain.Prioridades.Entities;

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
            usuario.Email = loginViewModel.Email;
                               
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
            var usuario = await _IUsuario.GetEntityById(id);
          
            return Criptografar(usuario);
        }

        private Usuario Criptografar(Usuario usuario){
            if (usuario != null)
                usuario.Password = Utils.Criptografia.Decriptografar(usuario.Password);
            else
                usuario = new Usuario();

           return usuario;

        }


        public bool IsUsuarioExiste(string login)
        {
            var usuario = this.ObterUsuario(login).Result;
            if (usuario == null )
               return false;
            else
               return true;
        }

        public async Task<List<Usuario>> List()
        {
            return await _IUsuario.List();
        }

        public async Task<Usuario> ObterUsuario(string login, string senha)
        {
            var usuario = await _IUsuario.ObterUsuario(login, senha);
            return  Criptografar(usuario);;
        }

        public async Task<Usuario> ObterUsuario(string login)
        {
            var usuario = await _IUsuario.ObterUsuario(login);
            return Criptografar(usuario);;
        }

        public async Task UpdateUsuario(LoginViewModel loginViewModel)
        {
             var usuario = new Domain.Prioridades.Entities.Usuario();
            usuario.Username = loginViewModel.Username;
            usuario.Password = Utils.Criptografia.CriptografarSenha(loginViewModel.Password);
            usuario.Email = loginViewModel.Email;
            usuario.Id = loginViewModel.Id;
            await _IUsuario.Update(usuario);
        }
    }
}
