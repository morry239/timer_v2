using Bank_API.Version;

namespace Bank_API.Fservices.Interfaces
{
    public interface TransactionServices
    {
        //create a response model 
        Response CreateNewTransaction(Transaction trs);
        Response SortTransactionByDate(DateTime date);
        Response Deposit(string AcNr, decimal Amt, string TrCode);
        Response Withdrawal(string AcNr, decimal Amt, string TrCode);
        Response FundsTransfer(string sendingAccount, string receivingAccount, decimal Amt, string TrCode);


    }
}
