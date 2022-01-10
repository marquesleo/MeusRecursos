using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
using ConsultaServer.Controllers.Filtros;
using Domain.Consulta.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using System;
using System.Threading.Tasks;

namespace ConsultaServer.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class AcessoController : BaseController
    {
        private readonly InterfaceAcessoApp _InterfaceAcessoApp;
        private readonly IMapper _mapper;

        public AcessoController(InterfaceAcessoApp InterfaceAcessoApp,
                                 IMapper mapper,
                                 INotificador notificador) : base(notificador)
        {
            _InterfaceAcessoApp = InterfaceAcessoApp;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var acesso = await _InterfaceAcessoApp.GetEntityById(id);

                return Ok(_mapper.Map<AcessoViewModel>(acesso));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

      
        
        [HttpGet]
        [Route("obteracessoporusuarioeempresa")]
        public async Task<IActionResult> ObterPorUsuarioEEmpresa([FromQuery] Guid idUsuario, [FromQuery] Guid idEmpresa)
        {
            try
            {
                var acesso =  await _InterfaceAcessoApp.ObterAcessoPorUsuarioEEmpresa(idUsuario, idEmpresa);
                 return Ok(_mapper.Map<AcessoViewModel>(acesso));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Add([FromBody] AcessoViewModel acessoViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {        
                   await _InterfaceAcessoApp.Incluir(acessoViewModel);
                   return CreatedAtAction(nameof(GetById), new { id = acessoViewModel.Id }, acessoViewModel);
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir acesso " + ex.Message);
            }

        }

        [HttpPut()]
        [AllowAnonymous]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Update([FromBody] AcessoViewModel acessoViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    await this._InterfaceAcessoApp.Alterar(acessoViewModel);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao alterar acesso " + ex.Message);
            }

        }
    }
}
