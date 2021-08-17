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

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppUsuario : InterfaceUsuarioApp
    {
        private readonly IUsuario _IUsuario; 
        public AppUsuario(IUsuario IUsuario)
        {
            this._IUsuario = IUsuario;
        }

        public async Task AddUsuario(Domain.Prioridades.Entities.Usuario usuario)
        {
            usuario.Password = new Utils.Hash(SHA512.Create()).CriptografarSenha(usuario.Password);
            await _IUsuario.Add(usuario);
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
            senha = new Utils.Hash(SHA512.Create()).CriptografarSenha(senha);
            return await _IUsuario.ObterUsuario(login, senha);
        }

        public async Task UpdateUsuario(Domain.Prioridades.Entities.Usuario usuario)
        {
            await _IUsuario.Update(usuario);
        }
    }
}
