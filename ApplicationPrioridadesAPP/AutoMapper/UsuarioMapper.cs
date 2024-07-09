using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System;

namespace AplicationPrioridadesAPP.AutoMapper
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            try
            {
                CreateMap<Usuario, LoginViewModel>()
              .ForMember(dest => dest.Username,
                         opt => opt.MapFrom(src => src.Username))
              .ForMember(dest => dest.Email,
                         opt => opt.MapFrom(src => src.Email))

              .ForMember(dest => dest.Id,
                         opt => opt.MapFrom(src => src.Id))

               .ForMember(dest => dest.Password,
                    opt => opt.MapFrom(src => Utils.Criptografia.Decriptografar(src.Password)));

             


                CreateMap<LoginViewModel, Usuario>()
                      .ForMember(dest => dest.Id,
                      opt => opt.MapFrom(src => src.Id))

                      .ForMember(dest => dest.Username,
                      opt => opt.MapFrom(src => src.Username))

                      .ForMember(dest => dest.Email,
                      opt => opt.MapFrom(src => src.Email))

                      .ForMember(dest => dest.Password,
                      opt => opt.MapFrom(src => Utils.Criptografia.CriptografarSenha(src.Password)))

                   

                     .ForMember(dest => dest.categoria, opt => opt.Ignore())
                     .ForMember(dest => dest.prioridade, opt => opt.Ignore())
                     .ForMember(dest => dest.senha, opt => opt.Ignore());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
    
}
