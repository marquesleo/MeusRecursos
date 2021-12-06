
using System.Threading.Tasks;
using System.Linq;
using Domain.Consulta.Entities;
using Domain.Consulta.Interface;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryUsuario :RepositoryGeneric<Usuario>, IUsuario
    {
        public RepositoryUsuario(MyDB myDB):base(myDB){ }

        public async Task<Usuario> ObterUsuario(string login, string senha)
        {
            var usuarios = await FindByCondition(p => p.Username.Equals(login) && 
                                                senha.Equals(Utils.Criptografia.Decriptografar(p.Password)));
          

            return usuarios.FirstOrDefault();
        }

        public async Task<Usuario> ObterUsuario(string login)
        {
            var usuarios = await FindByCondition(p => p.Username.Equals(login));
            return usuarios.FirstOrDefault();
        }
    }
}
