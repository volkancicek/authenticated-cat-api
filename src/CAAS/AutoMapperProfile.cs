using AutoMapper;
using CAAS.Entities;
using CAAS.Models;

namespace CAAS
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
        }
    }
}
