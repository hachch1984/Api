using Api.Dto;
using AutoMapper; 
using MinimalApiPeliculas.Entity;

namespace MinimalApiPeliculas.Utilities
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
