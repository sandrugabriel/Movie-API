using AutoMapper;
using MovieAPI.Dto;
using MovieAPI.Models;

namespace MovieAPI.Mappings
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {


            CreateMap<CreateRequest, Movie>();
        }

    }
}
