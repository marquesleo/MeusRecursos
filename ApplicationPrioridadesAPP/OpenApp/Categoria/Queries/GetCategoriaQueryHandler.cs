using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Categoria.Queries
{
    public class GetCategoriaQueryHandler : IRequestHandler<GetCategoriaQuery, CategoriaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceCategoriaApp _InterfaceCategoriaApp;
        public GetCategoriaQueryHandler(IMapper mapper,
                                        InterfaceCategoriaApp InterfaceCategoriaApp)
        {
            this._InterfaceCategoriaApp = InterfaceCategoriaApp;
            this._mapper = mapper;
        }

        public async Task<CategoriaResponse> Handle(GetCategoriaQuery request, CancellationToken cancellationToken)
        {
            var categoria = await _InterfaceCategoriaApp.GetEntityById(request.Id);

            if (categoria == null || categoria.Descricao == string.Empty)
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
                Data = _mapper.Map<Domain.Prioridades.ViewModels.CategoriaViewModel>(categoria),
                Success = true
            };

        }
    }
}
