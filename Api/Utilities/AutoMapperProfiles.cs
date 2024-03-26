using Api.Dto;
using AutoMapper; 
using Api.Entity;

namespace Api.Utilities
{
    public class AutoMapperProfiles:Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Persona, PersonaDtoAdd>().ReverseMap();
            CreateMap<Persona, PersonaDtoUpdate>().ReverseMap();  
             

        }
    }
}
