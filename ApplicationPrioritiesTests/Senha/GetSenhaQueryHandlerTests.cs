
using System;
using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Senha.Queries;
using AutoMapper;
using Moq;

namespace ApplicationPrioritiesTests.Senha
{
	public class GetSenhaQueryHandlerTests
	{

        public GetSenhaQueryHandler GetCommandMock(Mock<InterfaceSenhaApp> app)
        {
            var _app = app ?? new Mock<InterfaceSenhaApp>();

            var commandHandler = new GetSenhaQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }


        private readonly IMapper _mapper;
        public GetSenhaQueryHandlerTests()
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
        public async Task QuandoBuscoUmaSenhaPorId_RetornaSenha(string id)
        {
            var app = new Mock<InterfaceSenhaApp>();

            var query = new GetSenhaQuery
            {
                 Id = Guid.Parse(id)
            };

            var fake = new Domain.Prioridades.Entities.Senha
            {
                Usuario_Id = Guid.NewGuid(),
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
            app.Setup(x => x.GetEntityById(query.Id))
                .Returns(Task.FromResult(fake));

            var handler = GetCommandMock(app);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id,fake.Id);

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

