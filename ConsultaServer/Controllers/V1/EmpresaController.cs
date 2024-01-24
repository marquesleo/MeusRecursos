using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
using ConsultaServer.Controllers.Filtros;
using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using System;
using System.Threading.Tasks;

namespace ConsultaServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/empresa")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class EmpresaController : BaseController
    {


        private readonly InterfaceEmpresaApp _InterfaceEmpresaApp;
        private readonly IMapper _mapper;

        public EmpresaController(InterfaceEmpresaApp interfaceEmpresaApp,
                                     IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _InterfaceEmpresaApp = interfaceEmpresaApp;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok( _mapper.Map<EmpresaViewModel>(await _InterfaceEmpresaApp.GetEntityById(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Add([FromBody] EmpresaViewModel empresaviewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {      
                    
                    if (_InterfaceEmpresaApp.IsEmpresaExiste(empresaviewModel.Cnpj))
                    {
                        return BadRequest($"Empresa {empresaviewModel.Nome}  com CNPJ  {empresaviewModel.Cnpj} já está cadastrado!");
                    }

                    await _InterfaceEmpresaApp.Incluir(empresaviewModel);
                     return CreatedAtAction(nameof(GetById), new { id = empresaviewModel.Id }, empresaviewModel);
                    
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir empresa " + ex.Message);
            }

        }


        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] EmpresaViewModel empresaviewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var empresa = _mapper.Map<Empresa>(empresaviewModel);
                    await this._InterfaceEmpresaApp.Alterar(empresa);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao alterar empresa " + ex.Message);
            }

        }

    }
}
