using AutoMapper;
using UrlShortener.API.DTOs;
using UrlShortener.Models.Entities;

namespace UrlShortener.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        { 
            CreateMap<Url, UrlDto>().ReverseMap();
            CreateMap<Url, CreateUrlDto>().ReverseMap();
            CreateMap<Url, EditUrlDto>().ReverseMap();
        }
    }
}
