using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.EF
{
    [Table("tb_Contact")]
    public class Contact : CmAbtract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="tên không đượv bỏ trống")]
        [StringLength(150,ErrorMessage ="Không được vượt quá 150 kí tự")]
        public string? Name { get; set; }

        [StringLength(150, ErrorMessage = "Không được vượt quá 150 kí tự")]
        public string? Email{ get; set; }
        public string? Website { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }


    }
}
