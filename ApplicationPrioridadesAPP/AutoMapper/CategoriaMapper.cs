using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Prioridades.Entities;

namespace AplicationPrioridadesAPP.AutoMapper
{
	public class CategoriaMapper:Profile
	{
		public CategoriaMapper()
		{
            try
            {
                CreateMap<Categoria, Domain.Prioridades.ViewModels.CategoriaViewModel>()
              .ForMember(dest => dest.Descricao,
                         opt => opt.MapFrom(src => src.Descricao))
              .ForMember(dest => dest.Ativo,
                         opt => opt.MapFrom(src => src.Ativo))

              .ForMember(dest => dest.Id,
                         opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.NomeImagem,
                         opt => opt.MapFrom(src => src.NomeImagem))
              .ForMember(dest => dest.UsuarioId,
                         opt => opt.MapFrom(src => src.UsuarioId))

               .ForMember(dest => dest.ImagemData,
                         opt => opt.MapFrom(src => Encoding.UTF8.GetString(src.Imagem)))

              .ForMember(dest => dest.UrlImageSite,
                 opt => opt.MapFrom(src => src.UrlImageSite));


                CreateMap<Domain.Prioridades.ViewModels.CategoriaViewModel, Categoria>()
                    .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))

                    .ForMember(dest => dest.Descricao,
                    opt => opt.MapFrom(src => src.Descricao))

                    .ForMember(dest => dest.Ativo,
                    opt => opt.MapFrom(src => src.Ativo))

                    .ForMember(dest => dest.NomeImagem,
                    opt => opt.MapFrom(src => src.NomeImagem))

                    .ForMember(dest => dest.UrlImageSite,
                    opt => opt.MapFrom(src => src.UrlImageSite))

                    .ForMember(dest => dest.UsuarioId,
                    opt => opt.MapFrom(src => src.UsuarioId))

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

