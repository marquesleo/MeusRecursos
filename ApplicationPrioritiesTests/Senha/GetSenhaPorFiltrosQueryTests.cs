using System;
using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Queries;
using AutoMapper;
using Moq;

namespace ApplicationPrioritiesTests.Senha
{
	public class GetSenhaPorFiltrosQueryTests
	{
        private readonly IMapper _mapper;
        public GetSenhaPorFiltrosQueryTests()
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
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4","TESTE")]
        public async Task QuandoBuscoUmaSenhaPorFiltros_RetornaSenhas(string usuarioId, string descricao)
        {
            var app = new Mock<InterfaceSenhaApp>();

            var query = new GetSenhaPorFiltros
            {
                Descricao=  descricao,
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
                Id = Guid.NewGuid(),
                Categoria_Id = Guid.NewGuid(),
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

            app.Setup(x => x.ObterRegistrosPorFiltros(query.UsuarioId.ToString(),query.Descricao))
                .Returns(Task.FromResult(lst));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Lista.Count, lst.Count);

        }

        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4", "TESTE")]
        public async Task QuandoBuscoPorSenhaPorFiltros_NaoRetornaSenha(string usuarioId,string descricao)
        {
            var app = new Mock<InterfaceSenhaApp>();

            var query = new GetSenhaPorFiltros
            {
                UsuarioId = Guid.Parse(usuarioId),
                 Descricao =descricao
            };

            List<Domain.Prioridades.Entities.Senha> lstSenha = null;

            app.Setup(x => x.ObterRegistrosPorFiltros(query.UsuarioId.ToString(),query.Descricao))
                .Returns(Task.FromResult(lstSenha));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.SENHA_NOT_FOUND);

        }

        public GetSenhaPorFiltrosHandler GetCommandMock(Mock<InterfaceSenhaApp> app)
        {
            var _app = app ?? new Mock<InterfaceSenhaApp>();

            var commandHandler = new GetSenhaPorFiltrosHandler(_mapper, _app.Object);
            return commandHandler;
        }

    }
}

