using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Notification;
using System.Collections.Generic;
using Domain.Prioridades.Entities;
using AutoMapper;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.ViewModels;

namespace MinhasPrioridades.Controllers.V1
{
     [Route("api/v{version:apiVersion}/Senha")]
     [ApiController]
     [ApiVersion("1.0")]
    public class SenhaController : BaseController {

        private readonly IMapper _mapper;
      
         public SenhaController(IMapper mapper,
                                      INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }

    }
}