using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;


namespace ApplicationPrioritiesTests.Senha
{
    public class CreateSenhaCommandHandlerTests
    {


        private readonly IMapper _mapper;
        public CreateSenhaCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.SenhaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }

        public CreateSenhaCommandHandler GetCommandMock(Mock<InterfaceSenhaApp> app, Mock<NotificationContext> notificationContext)
        {
            var _app = app ?? new Mock<InterfaceSenhaApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();

            var commandHandler = new CreateSenhaCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479", "77590572-af8f-4ade-b8e1-868777b821c0")]
        public async Task QuandoCriaUmaSenhaValida_RetornaASenha(string usuario_id, string categoria_id)
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.NewGuid(),
                Usuario = usuario_id,
                Categoria = categoria_id,
                Observacao = "teste",
                Usuario_Site = "leonardo",
                Site = "http://www.teste.com",
                Password = "leonardo"
            };

            var cmd = new CreateSenhaCommand
            {
                SenhaViewModel = senhaDTO
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Id = senhaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id),
                Categoria_Id = Guid.Parse(senhaDTO.Categoria),
                Observacao = senhaDTO.Observacao,
                Password = senhaDTO.Password,
                Usuario_Site = senhaDTO.Usuario_Site,
                Site = senhaDTO.Site,

            };

            var appSenhaApp = new Mock<InterfaceSenhaApp>();

            appSenhaApp.Setup(x => x.AddSenha(It.IsAny<Domain.Prioridades.Entities.Senha>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(appSenhaApp, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fake.Id);

        }

        [Fact]
        public async Task QuandoCriaUmaSenhaComDescricaoVazia_RetornaAviso()
        {
           var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = string.Empty,
                Id = Guid.NewGuid(),
                Observacao = "teste",
                Usuario_Site = "leonardo",
                Site = "http://www.teste.com",
                Password = "leonardo"
           };

            var cmd = new CreateSenhaCommand
            {
                 SenhaViewModel =   senhaDTO
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Id = senhaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,
              
                Observacao = senhaDTO.Observacao,
                Password = senhaDTO.Password,
                Usuario_Site = senhaDTO.Usuario_Site,
                Site = senhaDTO.Site,

            };

            var appSenha = new Mock<InterfaceSenhaApp>();

            appSenha.Setup(x => x.AddSenha(It.IsAny<Domain.Prioridades.Entities.Senha>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(appSenha, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }


        [Fact]
        public async Task QuandoCriaUmaSenhaComUsuarioVazio_RetornaAviso()
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "teste",
                Id = Guid.NewGuid(),
                Observacao = "teste",
                Usuario_Site = string.Empty,
                Site = "http://www.teste.com",
                Password = "leonardo"
            };

            var cmd = new CreateSenhaCommand
            {
                SenhaViewModel = senhaDTO
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Id = senhaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,

                Observacao = senhaDTO.Observacao,
                Password = senhaDTO.Password,
                Usuario_Site = senhaDTO.Usuario_Site,
                Site = senhaDTO.Site,

            };

            var appSenha = new Mock<InterfaceSenhaApp>();

            appSenha.Setup(x => x.AddSenha(It.IsAny<Domain.Prioridades.Entities.Senha>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(appSenha, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }


        [Fact]
        public async Task QuandoCriaUmaSenhaComPasswordVazio_RetornaAviso()
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "teste",
                Id = Guid.NewGuid(),
                Observacao = "teste",
                Usuario_Site = "teste",
                Site = "http://www.teste.com",
               
            };

            var cmd = new CreateSenhaCommand
            {
                SenhaViewModel = senhaDTO
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Id = senhaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,

                Observacao = senhaDTO.Observacao,
                Password = senhaDTO.Password,
                Usuario_Site = senhaDTO.Usuario_Site,
                Site = senhaDTO.Site,

            };

            var appSenha = new Mock<InterfaceSenhaApp>();

            appSenha.Setup(x => x.AddSenha(It.IsAny<Domain.Prioridades.Entities.Senha>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(appSenha, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }

    }
}
