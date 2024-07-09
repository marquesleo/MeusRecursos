

using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.Prioridade
{
    public class DeletePrioridadeCommandHandlerTests
    {

        private readonly IMapper _mapper;
        public DeletePrioridadeCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public DeletePrioridadeCommandHandler GetCommandMock(Mock<InterfacePrioridadeApp> app, Mock<NotificationContext> notificationContext)
        {
            var _app = app ?? new Mock<InterfacePrioridadeApp>();

            var commandHandler = new DeletePrioridadeCommandHandler(_app.Object);
            return commandHandler;
        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", "77590572-af8f-4ade-b8e1-868777b821c0")]
        public async Task QuandoExcluoUmaPrioridadeValida_RetornaAPrioridadeExcluida(string usuario_id, string id)
        {
            var prioridadeDTO = new PrioridadeViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.Parse(id),
                Usuario = Guid.Parse(usuario_id)

            };

            var cmd = new DeletePrioridadeCommand
            {
                Id = prioridadeDTO.Id
            };

            var fakePrioridade = new Domain.Prioridades.Entities.Prioridade
            {
                Id = prioridadeDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var appPrioridade = new Mock<InterfacePrioridadeApp>();

            appPrioridade.Setup(x => x.Delete(fakePrioridade));
            appPrioridade.Setup(x => x.GetEntityById(fakePrioridade.Id)).Returns(Task.FromResult(fakePrioridade));

            var handler = GetCommandMock(appPrioridade, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);

        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", "77590572-af8f-4ade-b8e1-868777b821c0")]
        public async Task QuandoExcluoPrioridadeInValida_RetornaNotFound(string usuario_id, string id)
        {
            var prioridadeDTO = new PrioridadeViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.Parse(id),
                Usuario = Guid.Parse(usuario_id)
            };

            var cmd = new DeletePrioridadeCommand
            {
                Id = prioridadeDTO.Id
            };

            Domain.Prioridades.Entities.Prioridade fakePrioridade = null;

            var appPrioridade = new Mock<InterfacePrioridadeApp>();

            appPrioridade.Setup(x => x.Delete(fakePrioridade));
            appPrioridade.Setup(x => x.GetEntityById(prioridadeDTO.Id)).Returns(Task.FromResult(fakePrioridade));

            var handler = GetCommandMock(appPrioridade, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.PRIORIDADE_NOT_FOUND);

        }


    }
}
