using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Queries;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;

namespace ApplicationPrioritiesTests.Prioridade
{
    public class GetPrioridadeQueryHandlerTests
    {

        private readonly IMapper _mapper;
        public GetPrioridadeQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.PrioridadeMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public GetPrioridadeQueryHandler GetQyeryMock(Mock<InterfacePrioridadeApp> app)
        {
            var _app = app ?? new Mock<InterfacePrioridadeApp>();

            var commandHandler = new GetPrioridadeQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoPrioridadePorID_RetornaPrioridade(string id)
        {
            var guid = Guid.Parse(id);

            var appPrioridade = new Mock<InterfacePrioridadeApp>();

            var query = new GetPrioridadeQuery
            {
                Id = guid
            };

            var fakePrioridade = new Domain.Prioridades.Entities.Prioridade
            {
                Id = guid,
                Descricao = "TESTE",
                Ativo = true,
            };

            appPrioridade.Setup(x => x.GetEntityById(guid))
                .Returns(Task.FromResult(fakePrioridade));

            var handler = GetQyeryMock(appPrioridade);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fakePrioridade.Id);

        }

    }
}
