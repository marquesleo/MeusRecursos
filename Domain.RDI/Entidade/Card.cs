using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.RDI.Entidade
{
    [Table("card", Schema = "rdi")]
    public class Card
    {
        [Column("customerid", TypeName = "int")]
        public int CustomerId { get; set; }
        [Column("cardnumber", TypeName = "bigint")]
        public long CardNumber { get; set; }
        [Column("cvv", TypeName = "int")]
        public int CVV { get; set; }
    }
}