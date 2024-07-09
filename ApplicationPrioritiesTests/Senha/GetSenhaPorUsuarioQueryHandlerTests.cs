using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Queries;
using AutoMapper;
using Moq;


namespace ApplicationPrioritiesTests.Senha
{
    public class GetSenhaPorUsuarioQueryHandlerTests
    {

        private readonly IMapper _mapper;
        public GetSenhaPorUsuarioQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.SenhaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }



        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoUmaSenhaPorFiltroDeUsuario_RetornaSenhas(string usuarioId)
        {
            var app = new Mock<InterfaceSenhaApp>();

            var query = new GetSenhaPorUsuarioQuery
            {
                UsuarioId = Guid.Parse(usuarioId)
            };

            var lst = new List<Domain.Prioridades.Entities.Senha>();

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Usuario_Id = query.UsuarioId,
                Descricao = "TESTE",
                Ativo = true,
                Usuario_Site = "LEONARDO",
                Password = Utils.Criptografia.CriptografarSenha("SENHATESTE"),
                Site = "htt://www.globo.com",
                Id= Guid.NewGuid(),
                Categoria_Id =Guid.NewGuid(),
                Imagem = null,
                Observacao = "teste"
                    
            };

            lst.Add(fake);

            fake = new Domain.Prioridades.Entities.Senha()
            {
                Usuario_Id = query.UsuarioId,
                Descricao = "TESTE2",
                Ativo = true,
                Usuario_Site = "LUCIA",
                Password = Utils.Criptografia.CriptografarSenha("MINHASENHA"),
                Site = "htt://www.yahoo.com.br",
                Id = Guid.NewGuid(),
                Categoria_Id = Guid.NewGuid(),
                Imagem = null,
                Observacao = "teste"
            };
            lst.Add(fake);

            app.Setup(x => x.ObterRegistros(usuarioId))
                .Returns(Task.FromResult(lst));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Lista.Count, lst.Count);

        }

        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoPorSenhaPorUsuario_NaoRetornaSenha(string usuarioId)
        {
            var app = new Mock<InterfaceSenhaApp>();

            var query = new GetSenhaPorUsuarioQuery
            {
                 UsuarioId = Guid.Parse(usuarioId)
            };

            List<Domain.Prioridades.Entities.Senha>  lstSenha = null;

            app.Setup(x => x.ObterRegistros(usuarioId))
                .Returns(Task.FromResult(lstSenha));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.SENHA_NOT_FOUND);

        }



        public GetSenhaPorUsuarioQueryHandler GetCommandMock(Mock<InterfaceSenhaApp> app)
        {
            var _app = app ?? new Mock<InterfaceSenhaApp>();

            var commandHandler = new GetSenhaPorUsuarioQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }

    }
}
