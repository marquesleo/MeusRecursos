using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RDI.Entidade
{
    public class RespostaCard
    {
       public DateTime Registration { get; set; }
       public long Token { get; set; }
       public int CardId { get; set; }
    }
}
