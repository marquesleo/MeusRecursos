using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Exceptions;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;



namespace ApplicationPrioritiesTests.Categoria
{
    public class CreateCategoriaCommandHandlerTests
    {

        private readonly IMapper _mapper;
        public CreateCategoriaCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }

        public CreateCategoriaCommandHandler GetCommandMock(Mock<InterfaceCategoriaApp> app, Mock<NotificationContext> notificationContext)
        {
            var _app = app ?? new Mock<InterfaceCategoriaApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();

            var commandHandler = new CreateCategoriaCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }




        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandoCriaUmaCategoriaValida_RetornaACategoria(string usuario_id)
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.NewGuid(),
                Usuario_Id = Guid.Parse(usuario_id)

            };

            var cmd = new CreateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO
            };

            var fakeCategoria = new Domain.Prioridades.Entities.Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.AddCategoria(It.IsAny<Domain.Prioridades.Entities.Categoria> ()))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fakeCategoria.Id);

        }



        [Fact]
        public async Task QuandoCriaUmaCategoriaComDescricaoVazia_RetornaAviso()
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = string.Empty,
                Id = Guid.NewGuid()

            };

            var cmd = new CreateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO
            };

            var fakeCategoria = new Domain.Prioridades.Entities.Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = string.Empty,
                Ativo = true,

            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.AddCategoria(It.IsAny<Domain.Prioridades.Entities.Categoria>()))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);


        }



        [Fact]

        public async Task QuandoCriaUmaCategoriaJaCadastrada_RetornaException()
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.NewGuid()

            };

            var cmd = new CreateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO
            };

            var fakeCategoria = new Domain.Prioridades.Entities.Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,

            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.AddCategoria(It.IsAny<Domain.Prioridades.Entities.Categoria>()))
                .Callback(() => { throw new CategoriaDuplicateException(); });

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_DUPLICATE);



        }

    }
}
