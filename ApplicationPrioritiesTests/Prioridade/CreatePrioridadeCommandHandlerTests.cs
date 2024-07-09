using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.Prioridade
{
    public class CreatePrioridadeCommandHandlerTests
    {

        private readonly IMapper _mapper;
        public CreatePrioridadeCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.PrioridadeMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }

        public CreatePrioridadeCommandHandler GetCommandMock(Mock<InterfacePrioridadeApp> app, Mock<NotificationContext> notificationContext)
        {
            var _app = app ?? new Mock<InterfacePrioridadeApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();

            var commandHandler = new CreatePrioridadeCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandoCriaUmaPrioridadeValida_RetornaAPrioridade(string usuario_id)
        {
            var prioridadeDTO = new PrioridadeViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.NewGuid(),
                Usuario = Guid.Parse(usuario_id),
                Feito = false,
                Valor = 0
            };

            var cmd = new CreatePrioridadeCommand
            {
                 PrioridadeViewModel = prioridadeDTO
            };

            var fakePrioridade = new Domain.Prioridades.Entities.Prioridade
            {
                Id = prioridadeDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id),
                Valor = 0,
                Feito = false
            };

            var appPrioridade = new Mock<InterfacePrioridadeApp>();

            appPrioridade.Setup(x => x.AddPrioridade(It.IsAny<Domain.Prioridades.Entities.Prioridade>()))
                .Returns(Task.FromResult(fakePrioridade));

            var handler = GetCommandMock(appPrioridade, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fakePrioridade.Id);

        }

        [Fact]
        public async Task QuandoCriaUmaPrioridadeComDescricaoVazia_RetornaAviso()
        {
            var prioridadeDTO = new PrioridadeViewModel
            {
                Ativo = true,
                Descricao = string.Empty,
                Id = Guid.NewGuid()
            };

            var cmd = new CreatePrioridadeCommand
            {
                PrioridadeViewModel = prioridadeDTO
            };

            var fakePrioridade = new Domain.Prioridades.Entities.Prioridade
            {
                Id = prioridadeDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
                Valor = 0,
                Feito = false
            };

            var appPrioridade = new Mock<InterfacePrioridadeApp>();

            appPrioridade.Setup(x => x.AddPrioridade(It.IsAny<Domain.Prioridades.Entities.Prioridade>()))
                .Returns(Task.FromResult(fakePrioridade));

            var handler = GetCommandMock(appPrioridade, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }
    }
}
