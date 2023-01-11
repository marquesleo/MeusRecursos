using Contracts.Generics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prioridades.Interface
{
    public interface IPrioridade : IGeneric<Entities.Prioridade>
    { 
         Task Up(Entities.Prioridade prioridade); 

         Task Down(Entities.Prioridade prioridade); 

         Task InserirPrioridade(Entities.Prioridade prioridade);   

    }
}
