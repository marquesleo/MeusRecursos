using Contracts.Generics;
using Domain.Prioridades.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prioridades.Interface
{
    public interface IPrioridade : IGeneric<Entities.Prioridade>
    { 
         Task Up(Prioridade prioridade); 

         Task Down(Prioridade prioridade); 

         Task InserirPrioridade(Prioridade prioridade);

         Task SetOrder(Prioridade prioridade, enuOrdem ordem);

    }
}
