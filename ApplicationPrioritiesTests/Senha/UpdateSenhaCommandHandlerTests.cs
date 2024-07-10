
using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Senha.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.Senha
{
    public class UpdateSenhaCommandHandlerTests
    {
        private readonly IMapper _mapper;
        public UpdateSenhaCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.SenhaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public UpdateSenhaCommandHandler GetCommandMock(Mock<InterfaceSenhaApp> app, Mock<NotificationContext> notificationContext)
        {


            var _app = app ?? new Mock<InterfaceSenhaApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();


            var commandHandler = new UpdateSenhaCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaSenhaValida_RetornaASenha(string usuario_id)
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "TESTE2",
                Id = Guid.NewGuid(),
                Usuario = usuario_id,
                Usuario_Site = "leouser",
                Password = "123456"
            };

            var cmd = new UpdateSenhaCommand
            {
                 SenhaViewModel = senhaDTO,
                 Id = senhaDTO.Id
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Id = senhaDTO.Id,
                Descricao = "TESTE2",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id),
                Usuario_Site =  "leouser",
                Password = "123456"
            };

            var app = new Mock<InterfaceSenhaApp>();

            app.Setup(x => x.GetSenhaById(senhaDTO.Id.ToString())).Returns(Task.FromResult(fake));

            app.Setup(x => x.UpdateSenha(It.IsAny<Domain.Prioridades.Entities.Senha>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fake.Id);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaSenhaValidaQueNaoExiste_RetornaASenha(string usuario_id)
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "TESTE2",
                Id = Guid.NewGuid(),
                Usuario = usuario_id
            };

            var cmd = new UpdateSenhaCommand
            {
                SenhaViewModel = senhaDTO,
                Id = senhaDTO.Id
            };

            Domain.Prioridades.Entities.Senha fake = null;

            var app = new Mock<InterfaceSenhaApp>();

            app.Setup(x => x.GetSenhaById(senhaDTO.Id.ToString())).Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.SENHA_NOT_FOUND);

        }

       


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaSenhaComDadosNaoPreenchidos_RetornaDadosInvalidos(string usuario_id)
        {
            var senhaDTO = new SenhaViewModel
            {
                Ativo = true,
                Descricao = "TESTE2",
                Id = Guid.NewGuid(),
                Usuario = usuario_id
            };

            var cmd = new UpdateSenhaCommand
            {
                SenhaViewModel = senhaDTO,
                Id = senhaDTO.Id
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Id = senhaDTO.Id,
                Descricao = "TESTE2",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var app = new Mock<InterfaceSenhaApp>();

            app.Setup(x => x.GetSenhaById(senhaDTO.Id.ToString())).Returns(Task.FromResult(fake));
            app.Setup(x => x.UpdateSenha(It.IsAny<Domain.Prioridades.Entities.Senha>()))
              .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }

    }
}
