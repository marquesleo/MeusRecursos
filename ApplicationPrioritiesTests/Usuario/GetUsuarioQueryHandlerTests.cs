using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using AutoMapper;
using Moq;

namespace ApplicationPrioritiesTests.Usuario
{
    public class GetUsuarioQueryHandlerTests
    {
        private readonly IMapper _mapper;
        public GetUsuarioQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.UsuarioMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }

        public GetUsuarioQueryHandler GetCommandMock(Mock<InterfaceUsuarioApp> app,
                                                     IMapper mapper)
        {
            var _app = app ?? new Mock<InterfaceUsuarioApp>();

            var commandHandler = new GetUsuarioQueryHandler(_app.Object, mapper);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]

        public async Task QuandoBuscoUsuarioPorId_RetornaUsuario(string id)
        {
            var app = new Mock<InterfaceUsuarioApp>();

            var query = new GetUsuarioQuery
            {
                 Id = Guid.Parse(id)
            };

            var usuarioDTO = new Domain.Prioridades.Entities.Usuario()
            {
                Username = "leonardo",
                Password = Utils.Criptografia.CriptografarSenha("testeleonardo"),
                Email = "leo@gmail.com",
                Id = query.Id

            };

            app.Setup(x => x.ObterUsuario(query.Id))
                .Returns(Task.FromResult(usuarioDTO));

            var handler = GetCommandMock(app,_mapper);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Username, usuarioDTO.Username );

        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]

        public async Task QuandoTentaBuscarPorUsuarioInvalidoId_RetornaMensagemNaoEncontrado(string id)
        {
            var app = new Mock<InterfaceUsuarioApp>();

            var query = new GetUsuarioQuery
            {
                Id = Guid.Parse(id)
            };

            Domain.Prioridades.Entities.Usuario usuarioDTO = null;

            app.Setup(x => x.ObterUsuario(query.Id))
                .Returns(Task.FromResult(usuarioDTO));

            var handler = GetCommandMock(app, _mapper);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND);

        }

    }
}
