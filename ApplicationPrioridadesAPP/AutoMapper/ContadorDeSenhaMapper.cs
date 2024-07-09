using System;
using AutoMapper;
using Domain.Prioridades.Entities;

namespace AplicationPrioridadesAPP.AutoMapper
{
	public class ContadorDeSenhaMapper : Profile
    {
        public ContadorDeSenhaMapper()
        {
            try
            {
               CreateMap<ContadorDeSenha, Domain.Prioridades.ViewModels.ContadorSenhaViewModel>()
              .ForMember(dest => dest.contador,
                         opt => opt.MapFrom(src => src.Contador))


              .ForMember(dest => dest.dtAcesso,
                         opt => opt.MapFrom(src => src.DataDeAcesso))
              .ForMember(dest => dest.senhaId,
                         opt => opt.MapFrom(src => src.SenhaId));



                CreateMap<Domain.Prioridades.ViewModels.ContadorSenhaViewModel, ContadorDeSenha>()
                    .ForMember(dest => dest.Contador,
                    opt => opt.MapFrom(src => src.contador))

                    .ForMember(dest => dest.DataDeAcesso,
                    opt => opt.MapFrom(src => src.dtAcesso))

                    .ForMember(dest => dest.SenhaId,
                    opt => opt.MapFrom(src => src.senhaId));

                  

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}




