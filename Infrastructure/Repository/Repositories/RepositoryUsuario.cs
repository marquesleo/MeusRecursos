﻿using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryUsuario : Generics.RepositoryGeneric<Usuario>, IUsuario
    {
        public RepositoryUsuario(MyDB myDB):base(myDB){ }

        public async Task<Usuario> ObterUsuario(string login, string senha)
        {
            var usuarios = await FindByCondition(p => p.Username.Equals(login));
            Usuario usuario = null;
                if (usuarios != null && usuarios.Count > 0)
            {
               usuario =   usuarios.Where(p => senha.Equals(Utils.Criptografia.Decriptografar(p.Password))).FirstOrDefault();
            }

            return usuario;
        }
    }
}