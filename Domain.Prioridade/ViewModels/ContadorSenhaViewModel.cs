using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Prioridades.ViewModels
{
    public class ContadorSenhaViewModel
    {
        public Guid senhaId { get; set; }
        public DateTime dtAcesso { get; set; }
        public int contador { get; set; }
       
    }
}
