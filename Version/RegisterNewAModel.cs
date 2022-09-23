using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bank_API.Version

{
    public class RegisterNewAModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string OwnerName { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public decimal RemainingAccountBalance { get; set; }
        public AccountType AccountOption { get; set; } //enum to show if the type is savings or check
        //public string AccountNrGen { get; set; }

        //store the hash and salt of the transactions (why byte)
        //public byte[] HashPin { get; set; }

        //public byte[] CodeHash { get; set; }
        //public byte[] CodeSalt { get; set; }
        
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]/d{4}$", ErrorMessage = "Code must not exceed four digits")]
        public string Code { get; set; }
        [Required]
        [Compare("Code", ErrorMessage = "Code does not match")]
        public string ConfirmCode { get; set; }
    }
}
