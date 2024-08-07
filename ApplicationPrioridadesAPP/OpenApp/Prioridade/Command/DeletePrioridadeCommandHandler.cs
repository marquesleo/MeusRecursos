﻿
using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class DeletePrioridadeCommandHandler : IRequestHandler<DeletePrioridadeCommand, PrioridadeResponse>
    {
        
        private readonly InterfacePrioridadeApp _interfacePrioridadeApp;
        public DeletePrioridadeCommandHandler(InterfacePrioridadeApp InterfacePrioridadeApp)
        {
            this._interfacePrioridadeApp = InterfacePrioridadeApp;
           
        }
        public async Task<PrioridadeResponse> Handle(DeletePrioridadeCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var dbCategoria = await _interfacePrioridadeApp.GetEntityById(request.Id);

                if (dbCategoria == null || dbCategoria.Invalid)
                {
                    return new PrioridadeResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.PRIORIDADE_NOT_FOUND
                    };
                }
                else
                {
                    await _interfacePrioridadeApp.Delete(dbCategoria);

                    return new PrioridadeResponse
                    {
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
