
using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Queries;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;

namespace ApplicationPrioritiesTests.Prioridade
{
    public class GetAllPrioridadeQueryHandlerTests
    {
        private readonly IMapper _mapper;
        public GetAllPrioridadeQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.PrioridadeMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }

        public GetPrioridadeFiltrosQueryHandler GetQyeryMock(Mock<InterfacePrioridadeApp> app)
        {
            var _app = app ?? new Mock<InterfacePrioridadeApp>();

            var commandHandler = new GetPrioridadeFiltrosQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", false)]

        public async Task QuandoBuscoTodasPrioridadesPorUsuario_RetornaTodasPrioridades(string usuario_id, bool feito)
        {
            var appPrioridade = new Mock<InterfacePrioridadeApp>();

            var query = new GetPrioridadeFiltrosQuery
            {
                 Usuario_Id = usuario_id,
                 Feito= feito
            };

            List<Domain.Prioridades.Entities.Prioridade> fakelstPrioridade = new List<Domain.Prioridades.Entities.Prioridade>();

            fakelstPrioridade = GetPrioridade(usuario_id);
            var lstPrioridade = _mapper.Map<List<PrioridadeViewModel>>(fakelstPrioridade);

            appPrioridade.Setup(x => x.ObterPrioridade(usuario_id,feito))
                .Returns(Task.FromResult(lstPrioridade));

            var handler = GetQyeryMock(appPrioridade);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Lista.Count, lstPrioridade.Count);

        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandoBuscoTodasPrioridadesPorUsuario_RetornaNenhuma(string usuario_id)
        {
            var appPrioridade = new Mock<InterfacePrioridadeApp>();

            var query = new GetPrioridadeFiltrosQuery
            {
                Usuario_Id = usuario_id,
                Feito = false
            };


            List<PrioridadeViewModel> fakelstPrioridade = new List<PrioridadeViewModel>();

            appPrioridade.Setup(x => x.ObterPrioridade(query.Usuario_Id, query.Feito))
                .Returns(Task.FromResult(fakelstPrioridade));
            
            var handler = GetQyeryMock(appPrioridade);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.PRIORIDADE_NOT_FOUND);

        }


        private List<Domain.Prioridades.Entities.Prioridade> GetPrioridade(string id_usuario)
        {
            var retorno = new List<Domain.Prioridades.Entities.Prioridade>();
            var prioridade = new Domain.Prioridades.Entities.Prioridade()
            {
                Id = Guid.NewGuid(),
                Descricao = "TESTE01",
                Usuario_Id = Guid.Parse(id_usuario),
                Ativo = true,
                Feito = false,
                Valor = 1
            };
            retorno.Add(prioridade);
            prioridade = new Domain.Prioridades.Entities.Prioridade()
            {
                Id = Guid.NewGuid(),
                Descricao = "TESTE02",
                Usuario_Id = Guid.Parse(id_usuario),
                Ativo = true,
                Feito = false,
                Valor = 2
            };
            retorno.Add(prioridade);
            prioridade = new Domain.Prioridades.Entities.Prioridade()
            {
                Id = Guid.NewGuid(),
                Descricao = "TESTE03",
                Usuario_Id = Guid.Parse(id_usuario),
                Ativo = true,
                Feito = false,
                Valor = 3
            };
            retorno.Add(prioridade);
            return retorno;
        }

    }
}
