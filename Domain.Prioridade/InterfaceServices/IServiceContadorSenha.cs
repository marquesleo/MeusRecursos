using System;
using Domain.Prioridades.Entities;
using System.Threading.Tasks;

namespace Domain.Prioridades.InterfaceServices
{
	public interface IServiceContadorSenha
	{
        Task AddContador(ContadorDeSenha Senha);
        Task UpdateContador(ContadorDeSenha senha);
    }
}

