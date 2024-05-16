using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using Notification;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class UpPrioridadeCommandHandler : IRequestHandler<UpPrioridadeCommand, SenhaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfacePrioridadeApp _interfacePrioridadeApp;
        private readonly NotificationContext _notificationContext;

        public UpPrioridadeCommandHandler(IMapper mapper,
                                             InterfacePrioridadeApp InterfacePrioridadeApp,
                                             NotificationContext notificationContext)
        {
            this._interfacePrioridadeApp = InterfacePrioridadeApp;
            this._mapper = mapper;
            this._notificationContext = notificationContext;
        }
        public async Task<SenhaResponse> Handle(UpPrioridadeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var prioridade = _mapper.Map<Domain.Prioridades.Entities.Prioridade>(request.PrioridadeViewModel);
                if (prioridade != null && prioridade.Invalid)
                {
                    _notificationContext.AddNotifications(prioridade.ValidationResult);
                    return new SenhaResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                        Message = _notificationContext.GetErros()
                    };
                }
                else
                {
                    await _interfacePrioridadeApp.Up(prioridade);

                    return new SenhaResponse
                    {
                        Data = _mapper.Map<Domain.Prioridades.ViewModels.PrioridadeViewModel>(prioridade),
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

