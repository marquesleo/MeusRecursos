using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.Interfaces;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.InterfaceServices;
using Domain.Prioridades.Validations;
using Domain.Services;
using Notification;

namespace Domain.Prioridades.Services
{
	public class ServiceCategoria : BaseService, IServiceCategoria
    {
        private readonly ICategoria _ICategoria;

        public ServiceCategoria(ICategoria ICategoria,
                                  INotificador notificador) : base(notificador)
        {
            this._ICategoria = ICategoria;
        }

        public async Task AddCategoria(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;
            await _ICategoria.Add(categoria);
        }

        public async Task UpdateCategoria(Categoria categoria)
        {
            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return;
            if (_ICategoria.FindByCondition(p => p.Descricao == categoria.Descricao &&
                                 p.Usuario_Id == categoria.Usuario_Id &&
                                 p.Id != categoria.Id).Result.Any())
            {
                Notificar("Já existe uma categoria cadastrada!");
                return;
            }
            await _ICategoria.Update(categoria);
        }
    }
}

