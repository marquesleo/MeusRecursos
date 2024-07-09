using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Prioridade;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Command
{
    public class DeleteSenhaCommandHandler : IRequestHandler<DeleteSenhaCommand, SenhaResponse>
    {

        private readonly InterfaceSenhaApp _interfacSenhaApp;
        public DeleteSenhaCommandHandler(InterfaceSenhaApp InterfaceSenhaApp)
        {
            this._interfacSenhaApp = InterfaceSenhaApp;
          
        }
        public async Task<SenhaResponse> Handle(DeleteSenhaCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var dbSenha = await _interfacSenhaApp.GetEntityById(request.Id);

                if (dbSenha == null || !dbSenha.IdValido())
                {
                    return new SenhaResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.SENHA_NOT_FOUND
                    };
                }
                else
                {
                    await _interfacSenhaApp.Delete(dbSenha);

                    return new SenhaResponse
                    {
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
