using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bank_API.Version
{
    [Table("Accounts")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionRef { get; set; }//generate an instance off the Transasction class
        public string TransactionUniqueRef { get; set; }
        public decimal TransactionAmount { get; set; }
        public TrStatus TransactionStatus { get; set; }
        public bool IsCompleted => TransactionStatus.Equals(TrStatus.Success);//bool depends on the value of TrStatus
        public string TransactionSourceAccount { get; set; }
        public string TransactionParticulars   { get; set; }
        public TrType TransactionType { get; set; }
        public DateTime TrDate { get; set; }

        public Transaction()
        {
            //use a guid to generate the unique reference
            TransactionUniqueRef = $"{ Guid.NewGuid().ToString().Replace("-","").Substring(1,27)}";
        }

        public enum TrStatus
        {
            Failed,
            Success,
            Abort,//optional - how could the program function in case of abort? 
            Error
        }

        public enum TrType
        {
            Deposit,
            Withdrawal,
            Transfer
        }
    }
}
