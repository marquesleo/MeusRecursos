
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;

namespace ApplicationPrioridadesAPP.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            try
            {
                //CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
                //CreateMap<Usuario, LoginViewModel>().ReverseMap();
                //CreateMap<Senha, SenhaViewModel>().ReverseMap();
               // CreateMap <Prioridade, PrioridadeViewModel>().ReverseMap();
             
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            //CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
        }
        
    }
}
