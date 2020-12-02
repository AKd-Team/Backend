using AutoMapper;
using Academic.Entities;
using Academic.Models;
namespace Academic.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, LoginResponse>();
            CreateMap<RegisterRequest, Users>();
            CreateMap<RegisterStudent, Student>();
            CreateMap<RegisterAdmin, Admin>();
            CreateMap<RegisterProfesor, Profesor>();
            //CreateMap<UpdateRequest, User>();
        }
    }
}