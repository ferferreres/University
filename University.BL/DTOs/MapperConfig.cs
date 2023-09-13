using AutoMapper;
using University.BL.Models;
using University.BL.DTOs;

namespace University.BL.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>(); // GET
                cfg.CreateMap<CourseDTO, Course>(); // POST-PUT
            });
        }
    }
}
