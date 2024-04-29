using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Queries;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interfaces;
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
    public class GetAllCategoriaQueryHandlerTests
    {

        private readonly IMapper _mapper;
        public GetAllCategoriaQueryHandlerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                // Add your mappings here
                cfg.AddProfile<AplicationPrioridadesAPP.AutoMapper.CategoriaMapper>();
            });

            // Create IMapper instance
            _mapper = configuration.CreateMapper();
        }

        public GetAllCategoriaQueryHandler GetQyeryMock(Mock<InterfaceCategoriaApp> app)
        {
            var _app = app ?? new Mock<InterfaceCategoriaApp>();
            
            var commandHandler = new GetAllCategoriaQueryHandler(_mapper, _app.Object);
            return commandHandler;
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandoBuscoTodasCategoriasPorUsuario_RetornaTodasCategorias(string usuario_id)
        {
            var appCategoria = new Mock<InterfaceCategoriaApp>();

            var query = new GetAllCategoriaQuery
            {
                Id_Usuario = Guid.Parse(usuario_id)
            };

            List<Categoria> fakelstCategoria = new List<Categoria>();

            fakelstCategoria = GetCategorias(usuario_id);


            appCategoria.Setup(x => x.ObterCategoria(usuario_id))
                .Returns(Task.FromResult(fakelstCategoria));

            var lstMapper = _mapper.Map<List<CategoriaViewModel>>(fakelstCategoria);

            var handler = GetQyeryMock(appCategoria);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.True(resp.Success);
            Assert.Equal(resp.Lista.Count, lstMapper.Count);
          
        }


        [Theory]
        [InlineData("3944ec0a-173f-4606-8d65-d7ce4ce4f479")]
        public async Task QuandoBuscoTodasCategoriasPorUsuario_RetornaNenhuma(string usuario_id)
        {
            var appCategoria = new Mock<InterfaceCategoriaApp>();

            var query = new GetAllCategoriaQuery
            {
                Id_Usuario = Guid.Parse(usuario_id)
            };

            List<Categoria> fakelstCategoria = new List<Categoria>();
    
            appCategoria.Setup(x => x.ObterCategoria(usuario_id))
                .Returns(Task.FromResult(fakelstCategoria));

            var lstMapper = _mapper.Map<List<CategoriaViewModel>>(fakelstCategoria);

            var handler = GetQyeryMock(appCategoria);
            var resp = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resp);
            Assert.False(resp.Success);
            Assert.Equal(resp.ErrorCode,ApplicationPrioridadesAPP.ErrorCodes.CATEGORIA_NOT_FOUND);

        }


        private List<Categoria> GetCategorias(string id_usuario)
        {
            var retorno = new List<Categoria>();
            var categoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Descricao = "TESTE01",
                Usuario_Id = Guid.Parse(id_usuario),
                Ativo = true
            };
            retorno.Add(categoria);
            categoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Descricao = "TESTE02",
                Usuario_Id = Guid.Parse(id_usuario),
                Ativo = true
            };
            retorno.Add(categoria);
            categoria = new Categoria()
            {
                Id = Guid.NewGuid(),
                Descricao = "TESTE03",
                Usuario_Id = Guid.Parse(id_usuario),
                Ativo = true
            };
            retorno.Add(categoria);
            return retorno;
        }


    }
}
