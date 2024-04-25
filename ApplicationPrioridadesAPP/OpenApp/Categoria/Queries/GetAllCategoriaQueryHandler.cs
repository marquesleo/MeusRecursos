using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Queries
{
    public class GetAllCategoriaQueryHandler : IRequestHandler<GetAllCategoriaQuery, CategoriaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceCategoriaApp _InterfaceCategoriaApp;
        public GetAllCategoriaQueryHandler(IMapper mapper,
                                        InterfaceCategoriaApp InterfaceCategoriaApp)
        {
            this._InterfaceCategoriaApp = InterfaceCategoriaApp;
            this._mapper = mapper;
        }
        public async Task<CategoriaResponse> Handle(GetAllCategoriaQuery request, CancellationToken cancellationToken)
        {
            var categorias = await _InterfaceCategoriaApp.ObterCategoria(request.Id_Usuario.ToString());

            if (categorias == null || !categorias.Any() )
            {
                return new CategoriaResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.CATEGORIA_NOT_FOUND,
                    Message = ErrorCodes.CATEGORIA_NOT_FOUND.ToString()
                };

            }


            return new CategoriaResponse
            {
                Lista = _mapper.Map<List<Domain.Prioridades.ViewModels.CategoriaViewModel>>(categorias),
                Success = true
            };

        }
    }
}
