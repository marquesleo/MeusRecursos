using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Queries;
using AutoMapper;
using Domain.Prioridades.Entities;
using Moq;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioritiesTests.Categoria
{
    public class GetCategoriaQueryHandlerTests
    {
        private readonly IMapper _mapper;
        public GetCategoriaQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }



        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoCategoriaPorID_RetornaCategoria(string id)
        {
            var guid = Guid.Parse(id);

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            var query = new GetCategoriaQuery
            {
                Id = guid
            };

            var fakeCategoria = new Domain.Prioridades.Entities.Categoria
            {
                Id = guid,
                Descricao = "TESTE",
                Ativo = true,
            };

            appCategoria.Setup(x => x.GetEntityById(guid))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fakeCategoria.Id);

        }

        [Theory]
        [InlineData("000b1a3d-6669-4356-8c74-c561c95aebc4")]
        public async Task QuandoBuscoCategoriaPorID_NaoRetornaCategoria(string id)
        {

            var guid = Guid.Parse(id);

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            var query = new GetCategoriaQuery
            {
                Id = guid
            };

            Domain.Prioridades.Entities.Categoria fakeCategoria = null;

            appCategoria.Setup(x => x.GetEntityById(guid))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_NOT_FOUND);

        }



        public GetCategoriaQueryHandler GetCommandMock(Mock<InterfaceCategoriaApp> app, Mock<NotificationContext> notificationContext)
        {


            var _app = app ?? new Mock<InterfaceCategoriaApp>();

            var commandHandler = new GetCategoriaQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }


    }
}
