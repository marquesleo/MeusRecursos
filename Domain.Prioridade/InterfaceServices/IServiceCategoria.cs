using System;
using Domain.Prioridades.Entities;
using System.Threading.Tasks;

namespace Domain.Prioridades.InterfaceServices
{
	public interface IServiceCategoria
	{
        Task AddCategoria(Categoria Senha);
        Task UpdateCategoria(Categoria senha);
    }
}

