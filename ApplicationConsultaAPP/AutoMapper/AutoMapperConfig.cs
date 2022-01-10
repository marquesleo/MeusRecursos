
using ApplicationConsultaAPP.AutoMapper;
using AutoMapper;
using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;

namespace ApplicationConsultaAPP.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, LoginViewModel>().ReverseMap();
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();
            CreateMap<Acesso, AcessoViewModel>().ReverseMap();
            CreateMap<Psicologa, PsicologaViewModel>().ReverseMap();
            CreateMap<Consulta, ConsultaViewModel>().ReverseMap();
        }
        
    }
}
