

using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.Usuario
{
    public class UpdateUsuarioCommandHandlerTests
    {
        private readonly IMapper _mapper;
        public UpdateUsuarioCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.UsuarioMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public UpdateUsuarioCommandHandler GetCommandMock(Mock<InterfaceUsuarioApp> app, Mock<NotificationContext> notificationContext)
        {


            var _app = app ?? new Mock<InterfaceUsuarioApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();


            var commandHandler = new UpdateUsuarioCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaSenhaValida_RetornaASenha(string id)
        {
            var usuarioDTO = new LoginViewModel
            {
                Username = "leonardo",
                Password = Utils.Criptografia.CriptografarSenha("testeleonardo"),
                Email = "leo@gmail.com",
                Id =Guid.Parse(id)

            };

            var cmd = new UpdateUsuarioCommand
            {
               LoginViewModel = usuarioDTO,
               Id = usuarioDTO.Id
            };

            var fake = new Domain.Prioridades.Entities.Usuario
            {
                Username = "leonardo",
                Email = "leo@gmail.com",
                Password = Utils.Criptografia.CriptografarSenha("testeleonardo"),
                Id = usuarioDTO.Id
            };

            var app = new Mock<InterfaceUsuarioApp>();

            app.Setup(x => x.ObterUsuario(usuarioDTO.Id)).Returns(Task.FromResult(fake));

            app.Setup(x => x.UpdateUsuario(It.IsAny<Domain.Prioridades.Entities.Usuario>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fake.Id);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmUsaurioValidoQueNaoExiste_RetornaUsuarioInexistente(string id)
        {
            var usuarioDTO = new LoginViewModel
            {
                Username = "leonardo",
                Password = "testeleonardo",
                Email = "leo@gmail.com",
                Id = Guid.Parse(id)

            };

            var cmd = new UpdateUsuarioCommand
            {
                LoginViewModel = usuarioDTO,
                Id = usuarioDTO.Id
            };

            Domain.Prioridades.Entities.Usuario fake = null;

           
            var app = new Mock<InterfaceUsuarioApp>();

            app.Setup(x => x.GetEntityById(usuarioDTO.Id)).Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND);

        }




        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmUsuarioComDadosNaoPreenchidos_RetornaDadosInvalidos(string id)
        {
            var usuarioDTO = new LoginViewModel
            {
               
                Id = Guid.Parse(id)
            };

            var cmd = new UpdateUsuarioCommand
            {
                LoginViewModel = usuarioDTO,
                Id = usuarioDTO.Id
            };

            var fake = new Domain.Prioridades.Entities.Usuario
            {
              
                Id = usuarioDTO.Id
            };


            var app = new Mock<InterfaceUsuarioApp>();

            app.Setup(x => x.ObterUsuario(usuarioDTO.Id)).Returns(Task.FromResult(fake));
            app.Setup(x => x.UpdateUsuario(It.IsAny<Domain.Prioridades.Entities.Usuario>()))
              .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }
    }
}
