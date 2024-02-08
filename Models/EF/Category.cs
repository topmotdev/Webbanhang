
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.EF
{
    [Table("tb_Category")]
    public class Category : CmAbtract    
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh muc không được để trống")]
        public string? Title { get; set; }
        public string? Alias { get; set; }
        public string? Description { get; set; } 
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set;}
        public string? SeoKeywords { get; set; }
        public int Position { get; set; }
        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }    
        public Category()
        {
            this.News = new HashSet<New>();
            this.Posts =new HashSet<Post>();
        }

    }
}
