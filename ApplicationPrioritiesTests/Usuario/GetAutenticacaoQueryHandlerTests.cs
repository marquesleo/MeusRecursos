using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;

namespace ApplicationPrioritiesTests.Usuario
{
    public class GetAutenticacaoQueryHandlerTests
    {
        private readonly NotificationContext _notificationContext;
        public GetAutenticacaoQueryHandler GetCommandMock(Mock<InterfaceUsuarioApp> app, Mock<NotificationContext> notificationContext)
        {
            var _app = app ?? new Mock<InterfaceUsuarioApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();
            var commandHandler = new GetAutenticacaoQueryHandler(_app.Object, _mapper, _notificationContext.Object);
            return commandHandler;
        }


        private readonly IMapper _mapper;
        public GetAutenticacaoQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.UsuarioMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        [Theory]
        [InlineData("leonardo","Leo123456")]
        public async Task QuandoTentoAutenticarComLoginESenha_RetornaUsuarioAutenticado(string login, string senha)
        {
            var app = new Mock<InterfaceUsuarioApp>();

            var loginDTO = new LoginViewModel
            {
                Username = login,
                Password = senha,
                Email = "teste@teste.com"
            };
            var query = new GetAutenticacaoQuery
            {
               Login = loginDTO  
            };

            var fake = new Domain.Prioridades.Entities.Usuario
            {
                Username = loginDTO.Username,
                Password = loginDTO.Password,
                Email = loginDTO.Email
            };

            var authe = new Domain.Prioridades.Entities.AuthenticateResponse(fake, "", "", DateTime.Now.AddHours(8));
            
            app.Setup(x => x.ObterUsuario(query.Login.Username,query.Login.Password)).Returns(Task.FromResult(fake));
            
            
            app.Setup(x => x.Authenticate(It.IsAny<Domain.Prioridades.Entities.Usuario>(),It.IsAny<string>()))
                           .Returns(authe);


            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Username,fake.Username);

        }

        [Theory]
        [InlineData("leonardo", "Leo123456")]
        public async Task QuandoTentoAutenticarComLoginESenhaINVALIDOS_RetornaMensagemDeInvalidez(string login, string senha)
        {
              var app = new Mock<InterfaceUsuarioApp>();

            var loginDTO = new LoginViewModel
            {
                Username = login,
                Password = senha,
                Email = "teste@teste.com"
            };
            var query = new GetAutenticacaoQuery
            {
                Login = loginDTO
            };

            Domain.Prioridades.Entities.Usuario fake = null;

            Domain.Prioridades.Entities.AuthenticateResponse authe = null;

            app.Setup(x => x.ObterUsuario(query.Login.Username, query.Login.Password)).Returns(Task.FromResult(fake));


            app.Setup(x => x.Authenticate(It.IsAny<Domain.Prioridades.Entities.Usuario>(), It.IsAny<string>()))
                           .Returns(authe);


            var handler = GetCommandMock(app, null);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND);

        }
    }
}
