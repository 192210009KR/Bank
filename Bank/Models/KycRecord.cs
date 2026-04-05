using System.ComponentModel.DataAnnotations;
namespace Bank.Models
{
    public class KycRecord
    {
        [Key]
        public int KycId { get; set; }
        public int CustomerId { get; set; }
        public string DocType { get; set; }
        public string DocMasked { get; set; }
        public DateTime DOB { get; set; }
        public string KycStatus { get; set; }
        public string Reason { get; set; }
        public DateTime? VerifiedAt { get; set; }

        public Customer Customer { get; set; }
    }
}
