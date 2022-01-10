using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryAcesso : RepositoryGeneric<Acesso>, IAcesso
    {
        public RepositoryAcesso(MyDB myDB) : base(myDB) { }
        public async Task<Acesso> ObterAcessoPorId(Guid id)
        {
            try
            {
                var acesso = await FindByCondition(p => p.Id.Equals(id));
                return acesso.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Acesso> ObterAcessoPorUsuarioEEmpresa(Guid idUsuario, Guid idEmpresa)
        {

            try
            {
                var acesso = await FindByCondition(p => p.Usuario_Id == idUsuario && p.Empresa_Id == idEmpresa);
                return acesso.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
