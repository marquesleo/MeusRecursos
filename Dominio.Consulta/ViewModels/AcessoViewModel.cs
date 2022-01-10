using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.ViewModels
{
    public class AcessoViewModel
    {
        public Guid  Id { get; set; }

        public Guid Empresa_Id { get; set; }

        public Guid Usuario_Id { get; set; }

        public string Tipo { get; set; }
    }
}
