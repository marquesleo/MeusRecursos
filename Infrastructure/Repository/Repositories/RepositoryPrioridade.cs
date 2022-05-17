using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Infrastructure.Configuration;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryPrioridade : Generics.RepositoryGeneric<Prioridade>, IPrioridade
    {
        public RepositoryPrioridade(MyDB myDB):base(myDB){ }

        public async Task InserirPrioridade(Prioridade prioridade)
        {
           string sql = "call personal.sp_insertprioridade(@usuario, @descricao, @ativo);";
           await base.Exec(sql,prioridade);
           return; 
        }

         protected override async Task<List<object>> RetornarParametro(Prioridade objeto) {

            var lstParametro = new List<object>();
            lstParametro.Add(objeto.Usuario.Id);
             lstParametro.Add(objeto.Descricao);
            lstParametro.Add(objeto.Ativo);
           
            return lstParametro; 
        }

    }
}
