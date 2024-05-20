using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Queries
{
    public class GetSenhaPorFiltros : GetSenhaPorUsuarioQuery 
    {
        public string Descricao { get; set; }
    }
}
