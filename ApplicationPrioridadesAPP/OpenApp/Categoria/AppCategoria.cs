using Domain.Prioridades.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.Interfaces;
using Domain.Prioridades.InterfaceServices;


namespace ApplicationPrioridadesAPP.OpenApp.Categoria
{
    public class AppCategoria : InterfaceCategoriaApp
    {
        private readonly ICategoria _ICategoria;
        private readonly IUsuario _IUsuario;
        private readonly IServiceCategoria _IServiceCategoria;

        public AppCategoria(ICategoria ICategoria,
                            IUsuario IUsuario,
                            IServiceCategoria IServiceCategoria)
        {
            _ICategoria = ICategoria;
            _IServiceCategoria = IServiceCategoria;
            _IUsuario = IUsuario;
        }

        private async Task CarregarUsuario(Domain.Prioridades.Entities.Categoria categoria)
        {
            categoria.Usuario = await _IUsuario.GetEntityById(categoria.Usuario_Id);
        }


        public async Task AddCategoria(Domain.Prioridades.Entities.Categoria categoria)
        {
            //await CarregarUsuario(categoria);
            await _IServiceCategoria.AddCategoria(categoria);
        }

        public async Task Delete(Domain.Prioridades.Entities.Categoria objeto)
        {
            await _ICategoria.Delete(objeto);
        }

        public async Task<List<Domain.Prioridades.Entities.Categoria>> FindByCondition(Expression<Func<Domain.Prioridades.Entities.Categoria, bool>> expression)
        {
            return await _ICategoria.FindByCondition(expression);
        }

        public async Task<Domain.Prioridades.Entities.Categoria> GetEntityById(Guid id)
        {
            return await _ICategoria.GetEntityById(id);
        }

        public async Task<List<Domain.Prioridades.Entities.Categoria>> List()
        {
            return await _ICategoria.List();
        }

        public async Task<List<Domain.Prioridades.Entities.Categoria>> ObterCategoria(string id_usuario)
        {
            return await _ICategoria.FindByCondition(p => p.Usuario.Id == Guid.Parse(id_usuario));
        }

        public async Task UpdateCategoria(Domain.Prioridades.Entities.Categoria categoria)
        {
            await CarregarUsuario(categoria);
            await _IServiceCategoria.UpdateCategoria(categoria);
        }
    }
}

