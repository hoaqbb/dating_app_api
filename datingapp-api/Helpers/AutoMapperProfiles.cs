using AutoMapper;
using datingapp_api.Data.Entities;
using datingapp_api.DTOs;
using datingapp_api.Extensions;

namespace datingapp_api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<MemberUpdateDto, User>();

            CreateMap<Photo, PhotoDto>();
        }
    }
}
