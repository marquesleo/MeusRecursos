﻿using Domain.Prioridades.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Queries
{
    public class GetSenhaPorUsuarioQuery: IRequest<SenhaResponse>
    {
        public Guid UsuarioId {  get; set; }

       
    }
}
