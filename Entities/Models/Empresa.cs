using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{

    [Table("empresa", Schema = "identificacao")]
    public class Empresa : Base
    {
        [MaxLength(200,ErrorMessage = "Máximo de Caracteres")]
        private string razao {get;set;}



    }  
}
