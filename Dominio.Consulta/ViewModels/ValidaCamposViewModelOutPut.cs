using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.ViewModels
{
    public class ValidaCamposViewModelOutPut
    {
        public IEnumerable<string> Erros { get; private set; }
        public ValidaCamposViewModelOutPut(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
