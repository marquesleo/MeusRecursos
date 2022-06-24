using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Prioridades.Entities.Extension
{
    public static class PrioridadeExtension
   {
        public static void Map(this Prioridade dbPrioridade, ViewModels.PrioridadeViewModel  prioridadeViewModel)
        {
            dbPrioridade.Descricao = prioridadeViewModel.Descricao;
            dbPrioridade.Ativo = prioridadeViewModel.Ativo;
            dbPrioridade.Id = prioridadeViewModel.Id;
            dbPrioridade.Valor = prioridadeViewModel.Valor;
            dbPrioridade.Feito = prioridadeViewModel.Feito;
            dbPrioridade.Usuario = new Usuario();
            dbPrioridade.Usuario.Id = Guid.Parse(prioridadeViewModel.Usuario);
                    
        }
    }
}
