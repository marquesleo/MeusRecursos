using ApplicationAPP.ViewModels;
using AutoMapper;
using Domain.Prioridades.Entities;

namespace ApplicationAPP.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
           // CreateMap<Prioridade, PrioridadeViewModel>().ReverseMap();
        }
    }
}
