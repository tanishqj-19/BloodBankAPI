using System.ComponentModel.DataAnnotations;
namespace BloodBankAPI.Models
{
    public class BloodBankEntry
    {
        public int Id { get; set; }
        [StringLength(200)] public required string DonorName { get; set; }
        [Range(18, 65)] public int Age { get; set; } // Min Age: 18 , MaxAge 65
        public required string BloodType { get; set; }
        public string ContactInfo { get; set; } = string.Empty;
        [Range(1, long.MaxValue)] public decimal Quantity { get; set; }
        public DateTime CollectionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [StringLength(20)] public required String Status { get; set; }


    }


    
}
