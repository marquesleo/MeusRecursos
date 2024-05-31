using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Queries;
using ApplicationPrioridadesAPP.OpenApp.Senha.Queries;
using AutoMapper;
using Moq;
using Notification;


namespace ApplicationPrioritiesTests.Senha
{
    public class GetSenhaPorUsuarioQueryHandlerTests
    {

        private readonly IMapper _mapper;
        public GetSenhaPorUsuarioQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.SenhaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }



        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoUmaSenhaPorFiltroDeUsuario_RetornaSenhas(string usuarioId)
        {
            var guid = Guid.Parse(id);

            var appCategoria = new Mock<InterfaceSenhaApp>();

            var query = new GetSenhaPorUsuarioQuery
            {
                UsuarioId = Guid.Parse(usuarioId)
            };

            var fakeCategoria = new Domain.Prioridades.Entities.Categoria
            {
                Id = guid,
                Descricao = "TESTE",
                Ativo = true,
            };

            appCategoria.Setup(x => x.GetEntityById(guid))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fakeCategoria.Id);

        }

        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoCategoriaPorID_NaoRetornaCategoria(string id)
        {

            var guid = Guid.Parse(id);

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            var query = new GetCategoriaQuery
            {
                Id = guid
            };

            Domain.Prioridades.Entities.Categoria fakeCategoria = null;

            appCategoria.Setup(x => x.GetEntityById(guid))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_NOT_FOUND);

        }



        public GetCategoriaQueryHandler GetCommandMock(Mock<InterfaceCategoriaApp> app, Mock<NotificationContext> notificationContext)
        {


            var _app = app ?? new Mock<InterfaceCategoriaApp>();

            var commandHandler = new GetCategoriaQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }

    }
}
