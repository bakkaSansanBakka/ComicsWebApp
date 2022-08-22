using AutoMapper;
using ComicsWebApp.Data.Models;
using ComicsWebApp.Models;

namespace ComicsWebApp.Utilities
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<ComicsAddEditModel, ComicsViewModel>();
            CreateMap<Comics, ComicsViewModel>();
            CreateMap<ComicsAddEditModel, Comics>();
        }
    }
}
