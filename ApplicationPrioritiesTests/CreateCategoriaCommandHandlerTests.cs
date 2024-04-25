﻿using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Exceptions;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using Moq;
using Notification;
using static System.Net.Mime.MediaTypeNames;


namespace ApplicationPrioritiesTests
{
    public class CreateCategoriaCommandHandlerTests
    {

        private readonly IMapper _mapper;
        public CreateCategoriaCommandHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }


    

    public CreateCategoriaCommandHandler GetCommandMock(Mock<InterfaceCategoriaApp> app, Mock<NotificationContext> notificationContext)
        {
          

            var _app = app ?? new Mock<InterfaceCategoriaApp>();
            var _notificationContext = notificationContext ?? new Mock<NotificationContext>();


            var commandHandler = new CreateCategoriaCommandHandler(_mapper, _app.Object, _notificationContext.Object);
            return commandHandler;
        }



        [Fact]
        public async Task QuandoCriaUmaCategoriaValida_RetornaACategoria()
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.NewGuid()

            };

            var cmd = new CreateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO
            };

            var fakeCategoria = new Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,

            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.AddCategoria(It.IsAny<Categoria>()))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp =  await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Data.Id,fakeCategoria.Id);

        }



        [Fact]
        public async Task QuandoCriaUmaCategoriaComDescricaoVazia_RetornaAviso()
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = string.Empty,
                Id = Guid.NewGuid()

            };

            var cmd = new CreateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO
            };

            var fakeCategoria = new Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = string.Empty,
                Ativo = true,

            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();
                            
            appCategoria.Setup(x => x.AddCategoria(It.IsAny<Categoria>()))
                .Returns(Task.FromResult(fakeCategoria));

            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.MISSING_REQUIRED_INFORMATION);
            

        }



        [Fact]

        public async Task QuandoCriaUmaCategoriaJaCadastrada_RetornaException()
        {
            var categoriaDTO = new CategoriaViewModel
            {
                Ativo = true,
                Descricao = "TESTE",
                Id = Guid.NewGuid()

            };

            var cmd = new CreateCategoriaCommand
            {
                CategoriaViewModel = categoriaDTO
            };

            var fakeCategoria = new Categoria
            {
                Id = categoriaDTO.Id,
                Descricao = "TESTE",
                Ativo = true,

            };

            var appCategoria = new Mock<InterfaceCategoriaApp>();

            appCategoria.Setup(x => x.AddCategoria(It.IsAny<Categoria>()))
                .Callback(() => { throw new CategoriaDuplicateException(); });
           
            var handler = GetCommandMock(appCategoria, null);
            var resp = await handler.Handle(cmd, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode, ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_DUPLICATE);



        }

    }
}
