using System.ComponentModel.DataAnnotations;

namespace Bank_API.Version
{
    public class GetAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string OwnerName { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public decimal RemainingAccountBalance { get; set; }
        public AccountType AccountOption { get; set; } //enum to show if the type is savings or check
        public string AccountNrGen { get; set; }

        //store the hash and salt of the transactions (why byte)
        public byte[] HashPin { get; set; }

        public byte[] CodeHash { get; set; }
        public byte[] CodeSalt { get; set; }
        public byte[] Properties { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Ref { get; internal set; }

    }
}
