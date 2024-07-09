

using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.Interfaces.Generics;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Queries;
using ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha.Queries;
using AutoMapper;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.ContadorDeSenha
{
    public class GetContadorDeSenhaQueryHandlerTests
    {
        private readonly IMapper _mapper;
        public GetContadorDeSenhaQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.ContadorDeSenhaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoContadorDeSenhaPorIDdaSenha_RetornaContador(string idSenha)
        {
            var guid = Guid.Parse(idSenha);

            var app = new Mock<InterfaceContadorSenhaApp>();

            var query = new GetContadorDeSenhaQuery
            {
                 IdSenha = guid
            };

            var fake = new Domain.Prioridades.Entities.ContadorDeSenha
            {
                Contador = 1,
                DataDeAcesso = DateTime.Now,
                SenhaId = guid,
            };

            var lst = new List<Domain.Prioridades.Entities.ContadorDeSenha>();
            lst.Add(fake);

            app.Setup(x => x.GetContadorSenhaById(guid))
                .Returns(Task.FromResult(lst));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.List.Count, lst.Count);

        }


        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoContadorDeSenhaPorIDdaSenhaInvalido_RetornaContadorNotFound(string idSenha)
        {
            var guid = Guid.Parse(idSenha);

            var app = new Mock<InterfaceContadorSenhaApp>();

            var query = new GetContadorDeSenhaQuery
            {
                IdSenha = guid
            };
                      
            List<Domain.Prioridades.Entities.ContadorDeSenha> lst = null;

            app.Setup(x => x.GetContadorSenhaById(guid))
                .Returns(Task.FromResult(lst));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.CONTADOR_SENHA_NOT_FOUND);

        }


        public GetContadorDeSenhaQueryHandler GetCommandMock(Mock<InterfaceContadorSenhaApp> app)
        {


            var _app = app ?? new Mock<InterfaceContadorSenhaApp>();

            var commandHandler = new GetContadorDeSenhaQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }

    }
}
