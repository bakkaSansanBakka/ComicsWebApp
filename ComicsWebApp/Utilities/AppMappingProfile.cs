using AutoMapper;
using ComicsWebApp.Data.Models;
using ComicsWebApp.Models;

namespace ComicsWebApp.Utilities
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Comics, ComicsViewModel>()
                .ForMember(dest => dest.ListOfGenres, opt => opt.MapFrom(src => src.Genres))
                .ForMember(dest => dest.ListOfPages, opt => opt.MapFrom(src => src.Pages));

            CreateMap<ComicsViewModel, ComicsViewModel>();
        }
    }
}
