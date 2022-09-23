using Bank_API.Version;

namespace Bank_API.Fservices
{
    public interface AccountServices
    {
        Account Authentication(string AcNr, string Code);
        IEnumerable<Account> GetAccounts();
        Account Create(Account account, string Code, string CodeConfirm);
        void Update(Account account, string Code = null);
        void Delete(int Ref);
        Account GetById(int Ref);
        Account GetByAcNr(string AcNr);

    }
}
