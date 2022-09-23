using AutoMapper;
using Bank_API.Version;
namespace Bank_API.AutoMapper
{
    public class AMProfiles : Profile
    {
        public AMProfiles()
        {
            CreateMap<RegisterNewAModel, Account>();
            CreateMap<UpdateAModel, Account>();
            CreateMap<Account, GetAccountModel>();


        }
    }
}
