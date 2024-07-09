
using ApplicationPrioridadesAPP.Interfaces.Generics;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using MediatR;
using Notification;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha.Command
{
    public class UpdateContadorCommandHandler : IRequestHandler<UpdateContadorCommand, ContadorSenhaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceContadorSenhaApp _InterfaceContadorSenhaApp;
        

        public UpdateContadorCommandHandler(InterfaceContadorSenhaApp InterfaceContadorSenhaApp)
        {
            _InterfaceContadorSenhaApp = InterfaceContadorSenhaApp;
        }
        public async Task<ContadorSenhaResponse> Handle(UpdateContadorCommand request, CancellationToken cancellationToken)
        {
            try
            {
               

                var contadorSenha = await _InterfaceContadorSenhaApp.GetContadorSenhaById(request.ContadorSenhaViewModel.senhaId,
                                                                                          request.ContadorSenhaViewModel.dtAcesso);



                if (contadorSenha == null || contadorSenha.SenhaId == Guid.Empty)
                {
                    var contador = new Domain.Prioridades.Entities.ContadorDeSenha();
                    contador.SenhaId = request.ContadorSenhaViewModel.senhaId;
                    contador.Id = Guid.NewGuid();
                    contador.Contador = 1;
                    contador.DataDeAcesso = DateTime.Now;
                    await _InterfaceContadorSenhaApp.AddContador(contador);

                    return new ContadorSenhaResponse()
                    {
                        Data = _mapper.Map<ContadorSenhaViewModel>(contador),
                        Success = true,
                    };

                }
                else
                {
                    contadorSenha.Contador += 1;
                    await _InterfaceContadorSenhaApp.UpdateSenha(contadorSenha);
                    return new ContadorSenhaResponse()
                    {
                        Data = _mapper.Map<ContadorSenhaViewModel>(contadorSenha),
                        Success = true,
                    };
                }
            }
            catch (Exception ex)
            {

                return new ContadorSenhaResponse
                {
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }
}
