

using ApplicationPrioridadesAPP.AutoMapper;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.ViewModels;
using System;

namespace AplicationPrioridadesAPP.AutoMapper
{
    public class SenhaMapper : Profile
    {
        public SenhaMapper()
        {
            try
            {
                CreateMap<Senha, SenhaViewModel>()
              .ForMember(dest => dest.Descricao,
               opt => opt.MapFrom(src => src.Descricao))
              .ForMember(dest => dest.Ativo,
               opt => opt.MapFrom(src => src.Ativo))

              .ForMember(dest => dest.Id,
               opt => opt.MapFrom(src => src.Id))

              .ForMember(dest => dest.Observacao,
               opt => opt.MapFrom(src => src.Observacao))

              .ForMember(dest => dest.Usuario,
               opt => opt.MapFrom(src => src.Usuario_Id))

              .ForMember(dest => dest.Site,
                opt => opt.MapFrom(src => src.Site))

              .ForMember(dest => dest.Categoria,
               opt => opt.MapFrom(src => src.Categoria_Id))

              .ForMember(dest => dest.Password,
               opt => opt.MapFrom(src => src.Password))

              .ForMember(dest => dest.DtAtualizacao,
               opt => opt.MapFrom(src => src.DtAtualizacao))

              .ForMember(dest => dest.ImagemData,
              opt => opt.MapFrom(src => UtilMapper.getImagembase64String(src.Imagem)))

              .ForMember(dest => dest.NomeDaImagem,
               opt => opt.MapFrom(src => src.NomeImagem))
              
              
              .ForMember(dest => dest.Password,
                 opt => opt.MapFrom(src => Utils.Criptografia.Decriptografar(src.Password)))

              .ForMember(dest => dest.UrlImageSite,
               opt => opt.MapFrom(src => src.UrlImageSite));

               CreateMap<SenhaViewModel, Senha>()
              
              .ForMember(dest => dest.Id,
               opt => opt.MapFrom(src => src.Id))

               .ForMember(dest => dest.Descricao,
                opt => opt.MapFrom(src => src.Descricao))

               .ForMember(dest => dest.Ativo,
                opt => opt.MapFrom(src => src.Ativo))

               .ForMember(dest => dest.Observacao,
                opt => opt.MapFrom(src => src.Observacao))

               .ForMember(dest => dest.Usuario_Id,
                opt => opt.MapFrom(src => src.Usuario))

               .ForMember(dest => dest.DtAtualizacao,
               opt=> opt.MapFrom(src => src.DtAtualizacao))

                .ForMember(dest => dest.Categoria_Id,
                opt => opt.MapFrom(src => src.Categoria))

                .ForMember(dest => dest.Usuario, opt => opt.Ignore())
                
                .ForMember(dest => dest.NomeImagem,
                 opt => opt.MapFrom(src => src.NomeDaImagem))

                .ForMember(dest => dest.UrlImageSite,
                 opt => opt.MapFrom(src => src.UrlImageSite))

                .ForMember(dest => dest.Password,
                 opt => opt.MapFrom(src => Utils.Criptografia.CriptografarSenha(src.Password)))
                
                .ForMember(dest => dest.Imagem,
                opt => opt.MapFrom(src => Convert.FromBase64String(src.ImagemData)));


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    
    }
}
