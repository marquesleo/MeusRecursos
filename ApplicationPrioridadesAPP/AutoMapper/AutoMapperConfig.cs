
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;

namespace ApplicationPrioridadesAPP.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Prioridade, PrioridadeViewModel>().ReverseMap() ;
            CreateMap<Usuario, LoginViewModel>().ReverseMap();
             CreateMap<Senha, SenhaViewModel>().ReverseMap();
            //CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
        }
        
    }
}
