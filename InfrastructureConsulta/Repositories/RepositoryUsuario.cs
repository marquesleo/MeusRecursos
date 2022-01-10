
using System.Threading.Tasks;
using System.Linq;
using Domain.Consulta.Entities;
using Domain.Consulta.Interface;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using System;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryUsuario :RepositoryGeneric<Usuario>, IUsuario
    {
        public RepositoryUsuario(MyDB myDB):base(myDB){ }

        public async Task<Usuario> ObterUsuario(string login, string senha)
        {
            try
            {
                var usuarios = await FindByCondition(p => p.Username.Equals(login));
                if (usuarios != null && usuarios.Any())
                {
                    return usuarios.FirstOrDefault(p => p.Username.Equals(login) &&
                                            senha.Equals(Utils.Criptografia.Decriptografar(p.Password)));
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public async Task<Usuario> ObterUsuario(string login)
        {
            try
            {
                var usuarios = await FindByCondition(p => p.Username.Equals(login));
                return usuarios.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
    }
}
