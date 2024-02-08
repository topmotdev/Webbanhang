namespace WebApplication1.Models
{
    public abstract class CmAbtract
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set;}
        public DateTime? ModifiedrDate { get; set;}
        public string? ModifierBy { get; set;}

    }
}
