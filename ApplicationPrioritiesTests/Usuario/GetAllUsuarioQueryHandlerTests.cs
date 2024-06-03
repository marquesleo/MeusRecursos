

using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Queries;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using AutoMapper;
using Moq;

namespace ApplicationPrioritiesTests.Usuario
{
    public class GetAllUsuarioQueryHandlerTests
    {
        public GetAllUsuarioQueryHandler GetCommandMock(Mock<InterfaceUsuarioApp> app)
        {
            var _app = app ?? new Mock<InterfaceUsuarioApp>();

            var commandHandler = new GetAllUsuarioQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }


        private readonly IMapper _mapper;
        public GetAllUsuarioQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.UsuarioMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        [Fact]
       
        public async Task QuandoTodosUsuarios_RetornaListaDeUsuarios()
        {
            var app = new Mock<InterfaceUsuarioApp>();

            var query = new GetAllUsuarioQuery
            {
                
            };

            var usuarioDTO = new LoginViewModel
            {
                Username = "leonardo",
                Password = "testeleonardo",
                Email = "leo@gmail.com",
                Id = Guid.NewGuid()

            };
            app.Setup(x => x.GetEntityById(query.Id))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fake.Id);

        }

        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoSenhaPorId_NaoRetornaSenha(string id)
        {
            var app = new Mock<InterfaceSenhaApp>();

            var query = new GetSenhaQuery
            {
                Id = Guid.Parse(id)
            };

            Domain.Prioridades.Entities.Senha fake = null;

            app.Setup(x => x.GetEntityById(query.Id))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.SENHA_NOT_FOUND);

        }



    }
}
