using Contracts.Generics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prioridades.Interface
{
    public interface IPrioridade : IGeneric<Entities.Prioridade>
    { 

         Task InserirPrioridade(Entities.Prioridade prioridade);   

    }
}
