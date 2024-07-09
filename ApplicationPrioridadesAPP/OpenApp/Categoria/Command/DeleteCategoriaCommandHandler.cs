using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using Notification;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Command
{
    public class DeleteCategoriaCommandHandler : IRequestHandler<DeleteCategoriaCommand, CategoriaResponse>
    {
        private readonly IMapper _mapper;
        private readonly InterfaceCategoriaApp _InterfaceCategoriaApp;


        public DeleteCategoriaCommandHandler(IMapper mapper,
                                             InterfaceCategoriaApp InterfaceCategoriaApp)
        {
            this._InterfaceCategoriaApp = InterfaceCategoriaApp;
            this._mapper = mapper;
           
        }

        public async Task<CategoriaResponse> Handle(DeleteCategoriaCommand request, CancellationToken cancellationToken)
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
                    await _InterfaceCategoriaApp.Delete(dbCategoria);

                     return new CategoriaResponse
                     {
                            Success = true
                     };
                    
                }
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
