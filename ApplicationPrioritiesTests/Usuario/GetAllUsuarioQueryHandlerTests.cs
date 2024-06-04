

using ApplicationPrioridadesAPP.Interfaces;
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

            var usuarioDTO = new Domain.Prioridades.Entities.Usuario()
            {
                Username = "leonardo",
                Password = "testeleonardo",
                Email = "leo@gmail.com",
                Id = Guid.NewGuid()

            };

            var lst = new List<Domain.Prioridades.Entities.Usuario>();
            lst.Add(usuarioDTO);

            usuarioDTO = new Domain.Prioridades.Entities.Usuario()
            {
                Username = "joaomarques",
                Password = "123456",
                Email = "joao@gmail.com",
                Id = Guid.NewGuid()
            };
            lst.Add(usuarioDTO);
            usuarioDTO = new Domain.Prioridades.Entities.Usuario()
            {
                Username = "pedrohenrique",
                Password = "123456",
                Email = "joao@gmail.com",
                Id = Guid.NewGuid()
            };
            lst.Add(usuarioDTO);

            app.Setup(x => x.List())
                .Returns(Task.FromResult(lst));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Lista.Count, lst.Count);

        }

        [Fact]
        public async Task QuandoTentaBuscarTodosUsuarios_NaoRetornaNenhum()
        {
            var app = new Mock<InterfaceUsuarioApp>();

            var query = new GetAllUsuarioQuery
            {

            };

            
            List<Domain.Prioridades.Entities.Usuario> lst = null;
          
            app.Setup(x => x.List())
                .Returns(Task.FromResult(lst));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND);

        }



    }
}
