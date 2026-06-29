using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Domain.Entities;

namespace Dars_5ososy_API.Application.Mappings
{
    public class DomainProfile : Profile
    {
        public DomainProfile() {
            CreateMap<User, UserDTO>().ReverseMap()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == "M" ? "Male" : "Female"));

            CreateMap<SubjectDTO, Subject>().ReverseMap();
            CreateMap<EducationSystemDTO, EducationSystem>().ReverseMap();
            CreateMap<EducationStageDTO, EducationStage>().ReverseMap();

            CreateMap<ReviewDTO, Review>().ReverseMap()
                .ForMember(dest => dest.TeacherUsername, opt => opt.MapFrom(src => src.Teacher.UserName))
                .ForMember(dest => dest.StudentUsername, opt => opt.MapFrom(src => src.Student.UserName));
            
            CreateMap<FavoriteDTO, Favorite>().ReverseMap()
                .ForMember(dest => dest.StudentUsername, opt => opt.MapFrom(src => src.Student.UserName))
                .ForMember(dest => dest.TeacherUsername, opt => opt.MapFrom(src => src.Teacher.UserName));

            CreateMap<ProvinceDTO, Province>().ReverseMap();
            CreateMap<GovernorateDTO, Governorate>().ReverseMap();
            CreateMap<AreaDTO, Area>().ReverseMap();
        }
    }
}
