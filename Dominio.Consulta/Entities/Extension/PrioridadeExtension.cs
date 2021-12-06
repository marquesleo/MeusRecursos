using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Consulta.Entities.Extension
{
    public static class PrioridadeExtension
   {
        public static void Map(this Prioridade dbPrioridade, ViewModels.PrioridadeViewModel  prioridadeViewModel)
        {
            dbPrioridade.Descricao = prioridadeViewModel.Descricao;
            dbPrioridade.Ativo = prioridadeViewModel.Ativo;
            dbPrioridade.Id = prioridadeViewModel.Id;
            dbPrioridade.Valor = prioridadeViewModel.Valor;
                    
        }
    }
}
