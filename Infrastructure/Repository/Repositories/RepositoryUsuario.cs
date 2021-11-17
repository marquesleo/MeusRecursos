using Domain.Prioridades.Entities;
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
