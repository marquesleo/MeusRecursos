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



        private readonly InterfaceSenhaApp _InterfaceSenhaApp;
      
        public SenhaController(IMapper mapper,
                               INotificador notificador,
                               InterfaceSenhaApp InterfaceSenhaApp) : base(notificador)
        {
            _InterfaceSenhaApp = InterfaceSenhaApp;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _InterfaceSenhaApp.GetEntityById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SenhaViewModel senhaViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _InterfaceSenhaApp.AddSenha(senhaViewModel);
                    return CreatedAtAction(nameof(GetById), new { id = senhaViewModel.Id }, senhaViewModel);
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


    }
}