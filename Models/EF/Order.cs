using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.EF
{
    [Table("tb_Order")]
    public class Order : CmAbtract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Address {  get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
        public Order()
        {
            this.Details = new HashSet<OrderDetail>();
        }
    }
}
