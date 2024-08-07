﻿using System;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;

namespace AplicationPrioridadesAPP.AutoMapper
{
	public class PrioridadeMapper : Profile
    {
		public PrioridadeMapper()
		{
            try
            {
                CreateMap<Prioridade, PrioridadeViewModel>()
              .ForMember(dest => dest.Descricao,
                         opt => opt.MapFrom(src => src.Descricao))
              .ForMember(dest => dest.Ativo,
                         opt => opt.MapFrom(src => src.Ativo))

              .ForMember(dest => dest.Id,
                         opt => opt.MapFrom(src => src.Id))

              .ForMember(dest => dest.Feito,
                         opt => opt.MapFrom(src => src.Feito))

              .ForMember(dest => dest.Usuario,
                    opt => opt.MapFrom(src => src.Usuario_Id))

              .ForMember(dest => dest.Valor,
                opt => opt.MapFrom(src => src.Valor));

            
              CreateMap<PrioridadeViewModel, Prioridade>()
                    .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id))

                    .ForMember(dest => dest.Descricao,
                    opt => opt.MapFrom(src => src.Descricao))

                    .ForMember(dest => dest.Ativo,
                    opt => opt.MapFrom(src => src.Ativo))

                    .ForMember(dest => dest.Valor,
                    opt => opt.MapFrom(src => src.Valor))

                    .ForMember(dest => dest.Usuario_Id,
                    opt => opt.MapFrom(src => src.Usuario))

                    .ForMember(dest => dest.Feito,
                    opt => opt.MapFrom(src => src.Feito))
                                  
                   .ForMember(dest => dest.Usuario, opt => opt.Ignore());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

