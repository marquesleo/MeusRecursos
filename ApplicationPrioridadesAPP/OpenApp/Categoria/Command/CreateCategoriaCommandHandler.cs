
using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Exceptions;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Command
{
    public class CreateCategoriaCommandHandler : IRequestHandler<CreateCategoriaCommand, CategoriaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceCategoriaApp _InterfaceCategoriaApp;
        public CreateCategoriaCommandHandler(IMapper mapper,
                                             InterfaceCategoriaApp InterfaceCategoriaApp) 
        {
            this._InterfaceCategoriaApp = InterfaceCategoriaApp;
            this._mapper = mapper;
        }


        public async Task<CategoriaResponse> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var categoria = _mapper.Map<Domain.Prioridades.Entities.Categoria>(request.CategoriaViewModel);
                              
                await _InterfaceCategoriaApp.AddCategoria(categoria);


                return new CategoriaResponse
                {
                    Data = _mapper.Map<Domain.Prioridades.ViewModels.CategoriaViewModel>(categoria),
                    Success = true
                };

            }catch(CategoriaDuplicateException ex)
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
