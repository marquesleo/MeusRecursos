using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria;
using AutoMapper;
using Domain.Prioridades.Entities;
using MediatR;
using Notification;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
	public class CreatePrioridadeCommandHandler  : IRequestHandler<CreatePrioridadeCommand, PrioridadeResponse>
	{

        private readonly IMapper _mapper;
        private readonly InterfacePrioridadeApp _interfacePrioridadeApp;
        private readonly NotificationContext _notificationContext;



        public CreatePrioridadeCommandHandler(IMapper mapper,
                                             InterfacePrioridadeApp InterfacePrioridadeApp,
                                             NotificationContext notificationContext)
        {
            this._interfacePrioridadeApp = InterfacePrioridadeApp;
            this._mapper = mapper;
            this._notificationContext = notificationContext;
        }


        public async Task<PrioridadeResponse> Handle(CreatePrioridadeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var prioridade = _mapper.Map<Domain.Prioridades.Entities.Prioridade>(request.PrioridadeViewModel);
                if (prioridade != null && prioridade.Invalid)
                {
                    _notificationContext.AddNotifications(prioridade.ValidationResult);
                    return new PrioridadeResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                        Message = _notificationContext.GetErros()
                    };
                }
                else
                {
                    await _interfacePrioridadeApp.AddPrioridade(prioridade);

                    return new PrioridadeResponse
                    {
                        Data = _mapper.Map<Domain.Prioridades.ViewModels.PrioridadeViewModel>(prioridade),
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new PrioridadeResponse
                {
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}

