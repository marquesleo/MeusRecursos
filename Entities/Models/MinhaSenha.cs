using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("minhasenha", Schema = "person")]
    public class MinhaSenha:Base
    {
        private short codigo { get; set; }
        private string usuario { get; set; }
        private string email { get; set; }
        private string www { get; set; }
        private string obs { get; set; }
        private string senha { get; set; }
    }
}
