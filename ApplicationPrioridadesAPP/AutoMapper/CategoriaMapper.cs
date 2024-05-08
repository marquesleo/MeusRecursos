using AutoMapper;
using System;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;

namespace AplicationPrioridadesAPP.AutoMapper
{
	public class CategoriaMapper:Profile
    {
       private string getImagembase64String(byte[] imagem)
        {
              if (imagem != null && imagem.Length > 0)
                {
                    string base64String = Convert.ToBase64String(imagem, 0, imagem.Length);
                    return "data:image/png;base64," + base64String;
                }
            return null;
        }
       
		public CategoriaMapper()
		{
            try
            {
                CreateMap<Categoria, CategoriaViewModel>()
              .ForMember(dest => dest.Descricao,
                         opt => opt.MapFrom(src => src.Descricao))
              .ForMember(dest => dest.Ativo,
                         opt => opt.MapFrom(src => src.Ativo))

              .ForMember(dest => dest.Id,
                         opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.NomeImagem,
                         opt => opt.MapFrom(src => src.NomeImagem))
              .ForMember(dest => dest.Usuario_Id,
                         opt => opt.MapFrom(src => src.Usuario_Id))

               .ForMember(dest => dest.ImagemData,
                         opt => opt.MapFrom(src => getImagembase64String(src.Imagem)))

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

                    .ForMember(dest => dest.Usuario_Id,
                    opt => opt.MapFrom(src => src.Usuario_Id))

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

