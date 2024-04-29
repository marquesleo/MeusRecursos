using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Exceptions;
using AutoMapper;
using MediatR;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Command
{
    public class UpdateCategoriaCommandHandler : IRequestHandler<UpdateCategoriaCommand, CategoriaResponse>
    {
        private readonly IMapper _mapper;
        private readonly InterfaceCategoriaApp _InterfaceCategoriaApp;
        private readonly NotificationContext _notificationContext;

        public UpdateCategoriaCommandHandler(IMapper mapper,
                                             InterfaceCategoriaApp InterfaceCategoriaApp, 
                                             NotificationContext notificationContext)
        {
            this._InterfaceCategoriaApp = InterfaceCategoriaApp;
            this._mapper = mapper;
            this._notificationContext = notificationContext;
        }
        public async Task<CategoriaResponse> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var dbCategoria = await _InterfaceCategoriaApp.GetEntityById(request.Id);

                if (dbCategoria == null || dbCategoria.Invalid)
                {
                    return new CategoriaResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.CATEGORIA_NOT_FOUND
                    };
                }
                else
                {
                    var categoria = _mapper.Map<Domain.Prioridades.Entities.Categoria>(request.CategoriaViewModel);

                    if (categoria != null && categoria.Invalid)
                    {
                        _notificationContext.AddNotifications(categoria.ValidationResult);
                        return new CategoriaResponse
                        {
                            Success = false,
                            ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                            Message = _notificationContext.GetErros()
                        };
                    }
                    else
                    {
                        await _InterfaceCategoriaApp.UpdateCategoria(categoria);

                        return new CategoriaResponse
                        {
                            Data = _mapper.Map<Domain.Prioridades.ViewModels.CategoriaViewModel>(categoria),
                            Success = true
                        };
                    }
                }


               

            }
            catch (CategoriaDuplicateException ex)
            {
                return new CategoriaResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.CATEGORIA_DUPLICATE,
                    Message = ErrorCodes.CATEGORIA_DUPLICATE.ToString()
                };

            }
            catch (Exception ex)
            {

                return new CategoriaResponse
                {
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Success = false,
                    Message = ex.Message
                };
            }


        }
    }
}
