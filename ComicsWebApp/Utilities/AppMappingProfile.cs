using AutoMapper;
using ComicsWebApp.Data.Models;
using ComicsWebApp.Models;

namespace ComicsWebApp.Utilities
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ComicsAddEditModel, ComicsViewModel>().ReverseMap();
            CreateMap<Comics, ComicsViewModel>().ReverseMap();
            CreateMap<ComicsAddEditModel, Comics>().ReverseMap();
            CreateMap<ComicsPagesDTO, ComicsPages>().ReverseMap();
            CreateMap<ComicsGenreDTO, ComicsGenre>().ReverseMap();
        }
    }
}
