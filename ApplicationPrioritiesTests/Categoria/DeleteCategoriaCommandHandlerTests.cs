using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.Categoria
{
    public class DeleteCategoriaCommandHandlerTests
    {

        private readonly IMapper _mapper;
        public DeleteCategoriaCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public DeleteCategoriaCommandHandler GetCommandMock(Mock<InterfaceCategoriaApp> app, Mock<NotificationContext> notificationContext)
        {
            var _app = app ?? new Mock<InterfaceCategoriaApp>();

            var commandHandler = new DeleteCategoriaCommandHandler(_mapper, _app.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", "77590572-af8f-4ade-b8e1-868777b821c0")]
        public async Task QuandoExcluoCategoriaValida_RetornaACategoriaExcluida(string usuario_id, string id)
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.Parse(id),
                Usuario_Id = Guid.Parse(usuario_id)

            };

            var cmd = new DeleteCategoriaCommand
            {
                Id = categoriaDTO.Id
            };

            var fakeCategoria = new Domain.Prioridades.Entities.Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.Delete(fakeCategoria));
            appCategoria.Setup(x => x.GetEntityById(categoriaDTO.Id)).Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", "77590572-af8f-4ade-b8e1-868777b821c0")]
        public async Task QuandoExcluoCategoriaInValida_RetornaNotFound(string usuario_id, string id)
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.Parse(id),
                Usuario_Id = Guid.Parse(usuario_id)

            };

            var cmd = new DeleteCategoriaCommand
            {
                Id = categoriaDTO.Id
            };

            Domain.Prioridades.Entities.Categoria fakeCategoria = null;

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.Delete(fakeCategoria));
            appCategoria.Setup(x => x.GetEntityById(categoriaDTO.Id)).Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_NOT_FOUND);

        }


    }
}
