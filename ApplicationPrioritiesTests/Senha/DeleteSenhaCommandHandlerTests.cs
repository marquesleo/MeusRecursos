using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;

namespace ApplicationPrioritiesTests.Senha
{
    public class DeleteSenhaCommandHandlerTests
    {

        private readonly IMapper _mapper;
        public DeleteSenhaCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public DeleteSenhaCommandHandler GetCommandMock(Mock<InterfaceSenhaApp> app)
        {
            var _app = app ?? new Mock<InterfaceSenhaApp>();

            var commandHandler = new DeleteSenhaCommandHandler(_app.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", "77590572-af8f-4ade-b8e1-868777b821c0")]
        public async Task QuandoExcluoSenhaValida_RetornaASenhaExcluida(string usuario_id, string id)
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.Parse(id),
                Usuario = usuario_id

            };

            var cmd = new DeleteSenhaCommand
            {
                Id = senhaDTO.Id
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Id = senhaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var app = new Mock<InterfaceSenhaApp>();

            app.Setup(x => x.Delete(fake));
            app.Setup(x => x.GetEntityById(senhaDTO.Id)).Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", "77590572-af8f-4ade-b8e1-868777b821c0")]
        public async Task QuandoExcluoSenhaInValida_RetornaNotFound(string usuario_id, string id)
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.Parse(id),
                Usuario = usuario_id

            };

            var cmd = new DeleteSenhaCommand
            {
                Id = senhaDTO.Id
            };

            Domain.Prioridades.Entities.Senha fake = null;

            var app = new Mock<InterfaceSenhaApp>();

            app.Setup(x => x.Delete(fake));
            app.Setup(x => x.GetEntityById(senhaDTO.Id)).Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.SENHA_NOT_FOUND);

        }

    }
}
