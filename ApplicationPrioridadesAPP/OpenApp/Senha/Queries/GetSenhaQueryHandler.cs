using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Queries
{
    public class GetSenhaQueryHandler : IRequestHandler<GetSenhaQuery, SenhaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceSenhaApp _interfaceSenhaApp;

        public GetSenhaQueryHandler(IMapper mapper,
                                             InterfaceSenhaApp InterfaceSenhaApp
                                           )
        {
            this._interfaceSenhaApp = InterfaceSenhaApp;
            this._mapper = mapper;

        }
        public async Task<SenhaResponse> Handle(GetSenhaQuery request, CancellationToken cancellationToken)
        {
            var senha = await _interfaceSenhaApp.GetEntityById(request.Id);

            try
            {
                if (senha == null || senha.Descricao == string.Empty)
                {
                    return new SenhaResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.SENHA_NOT_FOUND,
                        Message = ErrorCodes.SENHA_NOT_FOUND.ToString()
                    };

                }

              //var semha = Utils.Criptografia.Decriptografar(senha.Password);
              //var senha2 = Utils.Criptografia.Decriptografar(semha);

                return new SenhaResponse
                {
                    Data = _mapper.Map<Domain.Prioridades.ViewModels.SenhaViewModel>(senha),
                    Success = true
                };

            }
            catch (System.Exception ex)
            {

                return new SenhaResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Message = ex.Message
                };
            }
        }
    }
}
