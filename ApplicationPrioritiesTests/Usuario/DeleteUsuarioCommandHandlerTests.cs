
using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Command;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Command;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;

namespace ApplicationPrioritiesTests.Usuario
{
    public class DeleteUsuarioCommandHandlerTests
    {
        private readonly IMapper _mapper;
        public DeleteUsuarioCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.UsuarioMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public DeleteUsuarioCommandHandler GetCommandMock(Mock<InterfaceUsuarioApp> app)
        {
            var _app = app ?? new Mock<InterfaceUsuarioApp>();

            var commandHandler = new DeleteUsuarioCommandHandler(_app.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandoExcluoUsuarioValido_RetornaUsuarioExcluido(string id)
        {
          
            var cmd = new DeleteUsuarioCommand
            {
                Id =  Guid.Parse(id)
            };


            var fake = new Domain.Prioridades.Entities.Usuario
            {
                Username = "leonardo",
                Email = "leo@gmail.com",
                Password = "testeleonardo",
                Id = Guid.Parse(id)
            };

            var app = new Mock<InterfaceUsuarioApp>();
             
            app.Setup(x => x.Delete(fake));
            app.Setup(x => x.ObterUsuario(fake.Id)).Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandoExcluoUsuarioInvalido_RetornaNotFound(string id)
        {

            var cmd = new DeleteUsuarioCommand
            {
                Id = Guid.Parse(id)
            };

            Domain.Prioridades.Entities.Usuario fake = null;

            var app = new Mock<InterfaceUsuarioApp>();

            app.Setup(x => x.Delete(fake));
            app.Setup(x => x.ObterUsuario(cmd.Id)).Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(cmd, CancellationToken.None);


            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND);

        }

    }
}
