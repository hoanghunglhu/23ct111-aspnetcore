using AutoMapper;
using StudentClassApi.Dtos;
using StudentClassApi.Models;

namespace StudentClassApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Class, ClassDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<CreateStudentDto, Student>();
        }
    }
}