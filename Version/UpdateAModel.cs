using System.ComponentModel.DataAnnotations;

namespace Bank_API.Version
{
    public class UpdateAModel
    {
        [Key]
        public int Id { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public decimal RemainingAccountBalance { get; set; }
        public DateTime LastUpdated { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]/d{4}$", ErrorMessage = "Code must not exceed four digits")]
        public string Code { get; set; }
        [Required]
        [Compare("Code", ErrorMessage = "Code does not match")]
        public string ConfirmCode { get; set; }
    }
}
