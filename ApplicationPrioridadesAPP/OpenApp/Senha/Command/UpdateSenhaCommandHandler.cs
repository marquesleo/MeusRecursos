using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Prioridade;
using AutoMapper;
using MediatR;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Command
{
    public class UpdateSenhaCommandHandler : IRequestHandler<UpdateSenhaCommand, SenhaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceSenhaApp _interfaceSenhaApp;
        private readonly NotificationContext _notificationContext;


        public UpdateSenhaCommandHandler(IMapper mapper,
                                            InterfaceSenhaApp InterfaceSenhaApp,
                                            NotificationContext notificationContext)
        {
            this._interfaceSenhaApp = InterfaceSenhaApp;
            this._mapper = mapper;
            this._notificationContext = notificationContext;
        }

        public async Task<SenhaResponse> Handle(UpdateSenhaCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var dbSenha = await _interfaceSenhaApp.GetSenhaById(request.Id.ToString());


                if (dbSenha == null || !dbSenha.IdValido())
                {
                    return new SenhaResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.NOT_FOUND,
                        Message = _notificationContext.GetErros()
                    };
                }

                var senha = _mapper.Map<Domain.Prioridades.Entities.Senha>(request.SenhaViewModel);
                if (senha != null && senha.Invalid)
                {
                    _notificationContext.AddNotifications(senha.ValidationResult);
                    return new SenhaResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                        Message = _notificationContext.GetErros()
                    };
                }
                else
                {
                    await _interfaceSenhaApp.UpdateSenha(senha);

                    return new SenhaResponse
                    {
                        Data = _mapper.Map<Domain.Prioridades.ViewModels.SenhaViewModel>(senha),
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new SenhaResponse
                {
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
