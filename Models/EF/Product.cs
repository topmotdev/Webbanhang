using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.EF
{
    [Table("tb_Product")]
    public class Product : CmAbtract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ProductCode { get; set; }
        public string? Title { get; set; }
        public string? Alias { get; set; }
        [ForeignKey("ProductCategory")]
        public int ProductCategoryID { get; set; }      
        public string? Description { get; set; }    
        public string? Detail { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }

        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public bool IsHome { get; set; }
        public bool IsActive { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? SeoKeywords { get; set; }
        public virtual ProductCategory?  ProductCategory { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    }
}
