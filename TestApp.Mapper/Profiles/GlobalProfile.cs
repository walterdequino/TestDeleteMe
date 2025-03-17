using AutoMapper;
using TestApp.Domain.Entities;
using TestApp.Domain.Entities.RandomUser;
using TestApp.Domain.Requests;
using TestApp.Domain.ViewModels;
using TestApp.Domain.ViewModels.RandomUser;

namespace TestApp.Mapper.Profiles
{
    /// <summary>
    /// Global Profile
    /// </summary>
    public class GlobalProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GlobalProfile()
        {
            // <source, destination>
            CreateMap<ObjectBase<dynamic>, ObjectViewModel>();
            CreateMap<CreateOrUpdateObjectRequest, CreateOrUpdateObjectBase>()
                .ForMember(static dest => dest.Data, static opt => opt.MapFrom(static sour => sour.Values));

            CreateMap<RandomUserName, RandomUserNameViewModel>();
            CreateMap<RandomUserLoginInfo, RandomUserLoginInfoViewModel>();
            CreateMap<RandomUser, RandomUserViewModel>();
        }
    }
}
