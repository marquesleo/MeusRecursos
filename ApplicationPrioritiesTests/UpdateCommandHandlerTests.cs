using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Exceptions;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioritiesTests
{
    public class UpdateCommandHandlerTests
    {



        private readonly IMapper _mapper;
        public UpdateCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


        public UpdateCategoriaCommandHandler GetCommandMock(Mock<InterfaceCategoriaApp> app, Mock<NotificationContext> notificationContext)
        {


            var _app = app ?? new Mock<InterfaceCategoriaApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();


            var commandHandler = new UpdateCategoriaCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaCategoriaValida_RetornaACategoria(string usuario_id)
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE2",
                Id = Guid.NewGuid(),
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var cmd = new UpdateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO,
                Id = categoriaDTO.Id
            };

            var fakeCategoria = new Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE2",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.GetEntityById(categoriaDTO.Id)).Returns(Task.FromResult(fakeCategoria));

            appCategoria.Setup(x => x.UpdateCategoria(It.IsAny<Categoria>()))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id, fakeCategoria.Id);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaCategoriaValidaQueNaoExiste_RetornaACategoria(string usuario_id)
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE2",
                Id = Guid.NewGuid(),
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var cmd = new UpdateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO,
                Id = categoriaDTO.Id
            };

            Categoria fakeCategoria = null;
          
            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.GetEntityById(categoriaDTO.Id)).Returns(Task.FromResult(fakeCategoria));
                      
            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode,ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_NOT_FOUND);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaCategoriaInvalida_RetornaAviso(string usuario_id)
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Id = Guid.NewGuid(),
                Descricao = "TESTE2",
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var cmd = new UpdateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO,
                Id = categoriaDTO.Id
            };

            var fakeCategoria = new Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE2",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.GetEntityById(categoriaDTO.Id)).Returns(Task.FromResult(fakeCategoria));
            appCategoria.Setup(x => x.UpdateCategoria(It.IsAny<Categoria>()))
               .Callback(() => { throw new CategoriaDuplicateException(); });

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_DUPLICATE);

        }

        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandAlteraUmaCategoriaDuplicada_RetornaException(string usuario_id)
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Id = Guid.NewGuid(),
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var cmd = new UpdateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO,
                Id = categoriaDTO.Id
            };

            var fakeCategoria = new Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE2",
                Ativo = true,
                Usuario_Id = Guid.Parse(usuario_id)
            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.GetEntityById(categoriaDTO.Id)).Returns(Task.FromResult(fakeCategoria));
            appCategoria.Setup(x => x.UpdateCategoria(It.IsAny<Categoria>()))
              .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);

        }





    }
}
