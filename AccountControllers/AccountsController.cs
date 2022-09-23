using Bank_API.AutoMapper;
using Bank_API.Fservices.Interfaces;
using Bank_API.Version;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Bank_API.AccountControllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class AccountsController : ControllerBase
    {
        //inject the Account Service component 
        private AccountServices _accService;
        //inject the Automapper also
        private AMProfiles _automapper;

        public AccountsController(AccountServices accService, AMProfiles automapper)
        {
            _accService = accService;
            _automapper = automapper;
        }

        [HttpPost]
        [Route("register_new_account")]

        public IActionResult RegNewAcc([FromBody] RegisterNewAModel newAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(newAccount);
            }

            var account = _automapper.Map<Account>(newAccount);
            return Ok(_accService.Create(account, newAccount.Code, newAccount.ConfirmCode));
        }

        [HttpGet]
        [Route("grab_all_accounts")]
        public IActionResult GetAllAccounts()
        {
            //map to the GetAccountModel class
            var accounts = _accService.GetAllAccounts();
            var accountCleaned = _automapper.Map<IList<GetAccountModel>>(accounts);
            return Ok(accountCleaned);
        }
    }

    
}
