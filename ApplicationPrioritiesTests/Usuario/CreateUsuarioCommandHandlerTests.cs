

using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Command;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.Usuario
{
    public class CreateUsuarioCommandHandlerTests
    {

        private readonly IMapper _mapper;
        public CreateUsuarioCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.UsuarioMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }

        public CreateUsuarioCommandHandler GetCommandMock(Mock<InterfaceUsuarioApp> app, Mock<NotificationContext> notificationContext)
        {
            var _app = app ?? new Mock<InterfaceUsuarioApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();

            var commandHandler = new CreateUsuarioCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }

        [Fact]
      
        public async Task QuandoCriaUmaUsuarioValida_RetornaAUsuario()
        {
            var usuarioDTO = new LoginViewModel
            {
                Username = "leonardo",
                Password = "testeleonardo",
                Email = "leo@gmail.com",
                Id = Guid.NewGuid()
                
            };
            var cmd = new CreateUsuarioCommand
            {
                LoginViewModel = usuarioDTO
            };

            var fake = new Domain.Prioridades.Entities.Usuario
            {
                Username = "leonardo",
                Email = "leo@gmail.com",
                Password = "testeleonardo",
                Id = usuarioDTO.Id
            };

            var app = new Mock<InterfaceUsuarioApp>();

            app.Setup(x => x.AddUsuario(It.IsAny<Domain.Prioridades.Entities.Usuario>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fake.Id);

        }

        [Fact]
        public async Task QuandoCriaUmUsuarioComLoginVazio_RetornaAviso()
        {
            var usuarioDTO = new LoginViewModel
            {
                Username = string.Empty,
                Password = string.Empty,
                Email = string.Empty,
                Id = Guid.NewGuid()

            };
            var cmd = new CreateUsuarioCommand
            {
                LoginViewModel = usuarioDTO
            };

            var fake = new Domain.Prioridades.Entities.Usuario
            {
                Username = string.Empty,
                Password = string.Empty,
                Email = string.Empty,
                Id = Guid.NewGuid()
            };

            var app = new Mock<InterfaceUsuarioApp>();

            app.Setup(x => x.AddUsuario(It.IsAny<Domain.Prioridades.Entities.Usuario>()))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }

    }
}
