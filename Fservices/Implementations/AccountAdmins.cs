using Bank_API.Version;
using Bank_API.DB;
using System.Text;

namespace Bank_API.Fservices.Implementations
{
    public class AccountAdmins : AccountServices
    {
        private ApplicationContext _dbContext;
        public AccountAdmins(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account Authentication(string AcNr, string Code)
        {
            //check if the account exists for a specific account number 
            var account = _dbContext.Accounts.Where(x => x.AccountNrGen == AcNr).SingleOrDefault();
            if (account == null)
            {
                return null;
            }
            //the account exists 
            //verify hash code (or hashpin) 
            if (!VerifyCodeHash(Code, account.CodeHash, account.CodeSalt))
            {
                return null;
            }

            //authentication passed
            return account;
        }

        public static bool VerifyCodeHash(string Code, byte[] CodeHash, byte[] CodeSalt)
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                throw new ArgumentNullException("Code");
            }
               
                    //verify pin/code
                    using (var crypto = new System.Security.Cryptography.HMACSHA512())
                    {
                        var computedCodeHash = crypto.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Code));
                        for(int i = 0; i < computedCodeHash.Length; i++)
                        {
                            if (computedCodeHash[i] != CodeHash[i])
                            {
                                return false;
                            }
                        }
                    }
            return true;

           
        }

        public Account Create(Account account, string Code, string CodeConfirm)
        {
            //create a new account
            if (_dbContext.Accounts.Any(x => x.Mail == account.Mail))
            {
                throw new ApplicationException("account dupolicated");
            }
            //validation failed
            if(!Code.Equals(CodeConfirm))
            {
                throw new ArgumentException("invalid codes or pwd", "code");
            }
            //validation passed
            byte[] codeHash, codeSalt;
            //create a crypto method
            CreateCodeHash(Code, out codeHash, out codeSalt);

            account.CodeHash = codeHash;
            account.CodeSalt = codeSalt;

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            return account;
        }

        private static void CreateCodeHash(string Code, out byte[] codeHash, out byte[] codeSalt)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                codeSalt = crypto.Key;
                codeHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(Code));
                

            }
        }

        public void Delete(int Ref)
        {
            var account = _dbContext.Accounts.Find(Ref);
            if(account != null)
            {
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

        public Account GetByAcNr(string AcNr)
        {
            var account = _dbContext.Accounts.Where(x => x.AccountNrGen == AcNr).FirstOrDefault();
            if(account == null)
            {
                return null;
            }
            return account;
        }

        public Account GetById(int Ref)
        {
            var account = _dbContext.Accounts.Where(x => x.Ref == Ref).FirstOrDefault();
            if(account == null)
            {
                return null;
            }
            return account;
        }

        public void Update(Account account, string Code = null)
        {
            var accountToBeaUpdated = _dbContext.Accounts.Where(x => x.Mail == account.Mail).SingleOrDefault();
            if(accountToBeaUpdated == null)
            {
                throw new ApplicationException("no such account exists");
            }
            //if an account to be updated exists, check if the account wants to change any of its properties
            //for example change of emails
            if (!string.IsNullOrWhiteSpace(account.Mail))
            {
                //check if the email is already taken
                if(_dbContext.Accounts.Any(x => x.Mail == account.Mail))
                {
                    throw new ApplicationException("email already exists");
                }
                //a new email there, apply the change
                accountToBeaUpdated.Mail = account.Mail;

            }

            //change only email, phone number, and code
            //(ideas for weiterentwicklung: change the user name/legal name also)
            if (!string.IsNullOrWhiteSpace(account.Telephone))
            {
                //repeat the same logic for change phone numbers too
                if (_dbContext.Accounts.Any(x => x.Telephone == account.Telephone))
                {
                    throw new ApplicationException("email already exists");
                }
                accountToBeaUpdated.Telephone = account.Telephone;

            }

            //change codes/pins
            if (!string.IsNullOrWhiteSpace(Code))
            {
                byte[] codeHash, codeSalt;
                
                CreateCodeHash(Code, out codeHash, out codeSalt);

                accountToBeaUpdated.CodeHash = codeHash;
                accountToBeaUpdated.CodeSalt = codeSalt;
            }
            accountToBeaUpdated.LastUpdated = DateTime.Now;

            _dbContext.Accounts.Add(accountToBeaUpdated);
            _dbContext.SaveChanges();
        }

        
    }
}
