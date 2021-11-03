using AutoMapper;
using BO = SocialNetwork.ProfileManager.BusinessObjects;
using EO = SocialNetwork.ProfileManager.Entities;

namespace SocialNetwork.ProfileManager.Profiles
{
    class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<EO.Member, BO.Member>().ReverseMap();
            CreateMap<EO.Photo, BO.Photo>().ReverseMap();
        }
    }
}
