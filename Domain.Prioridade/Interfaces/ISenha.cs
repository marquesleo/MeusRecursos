using Contracts.Generics;
using System.Threading.Tasks;
using Domain.Prioridades.Entities;
namespace Domain.Prioridades.Interface
{
    public interface ISenha : IGeneric<Senha>
    { 
         Task Inserir(Entities.Senha senha);   
         Task Alterar(Entities.Senha senha);  
         Task Excluir(Entities.Senha senha); 

    }
}
