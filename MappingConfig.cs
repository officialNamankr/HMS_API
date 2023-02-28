using AutoMapper;
using HMS_API.Models;
using HMS_API.Models.Dto;

namespace HMS_API
{
    public class MappingConfig :Profile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Department, DepartmentViewDto>();
                
            });
            return mappingConfig;
        }
    }
}
