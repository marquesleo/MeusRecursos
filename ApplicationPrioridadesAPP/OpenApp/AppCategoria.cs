using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.Interfaces;
using Domain.Prioridades.InterfaceServices;

namespace ApplicationPrioridadesAPP.OpenApp
{
	public class AppCategoria: InterfaceCategoriaApp
	{
        private readonly ICategoria _ICategoria;
        private readonly IUsuario _IUsuario;
        private readonly IServiceCategoria _IServiceCategoria;
    
        public AppCategoria(ICategoria ICategoria,
                            IUsuario IUsuario,
                            IServiceCategoria IServiceCategoria)
		{
            this._ICategoria = ICategoria;
            this._IServiceCategoria = IServiceCategoria;
            this._IUsuario = IUsuario;
		}

        private async Task CarregarUsuario(Categoria categoria)
        {
            categoria.Usuario = await _IUsuario.GetEntityById(categoria.UsuarioId);
        }


        public async Task AddCategoria(Categoria categoria)
        {
            await CarregarUsuario(categoria);
            await _IServiceCategoria.AddCategoria(categoria);
        }

        public async Task Delete(Categoria objeto)
        {
            await _ICategoria.Delete(objeto);
        }

        public async Task<List<Categoria>> FindByCondition(Expression<Func<Categoria, bool>> expression)
        {
            return await _ICategoria.FindByCondition(expression);
        }

        public async Task<Categoria> GetEntityById(Guid id)
        {
            return await _ICategoria.GetEntityById(id);
        }

        public  async  Task<List<Categoria>> List()
        {
            return await _ICategoria.List();
        }

        public async Task<List<Categoria>> ObterCategoria(string id_usuario)
        {
          return  await _ICategoria.FindByCondition(p => p.Usuario.Id == Guid.Parse(id_usuario));
        }

        public async Task UpdateCategoria(Categoria categoria)
        {
            await CarregarUsuario(categoria);
            await _IServiceCategoria.UpdateCategoria(categoria);
        }
    }
}

