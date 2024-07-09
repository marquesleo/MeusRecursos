using ApplicationPrioridadesAPP.Interfaces.Generics;
using Domain.Prioridades.Interface;
using Domain.Prioridades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha
{
    public class AppContadorSenha : InterfaceContadorSenhaApp
    {

        private readonly IContadorSenha _IContadorSenha;
        private readonly IUsuario _IUsuario;



        public AppContadorSenha(IContadorSenha IContadorSenha,
                           IUsuario IUsuario)
        {
            _IContadorSenha = IContadorSenha;
            _IUsuario = IUsuario;
        }


        public async Task AddContador(Domain.Prioridades.Entities.ContadorDeSenha contadorSenha)
        {
            await _IContadorSenha.Add(contadorSenha);
        }

        public async Task Delete(Domain.Prioridades.Entities.ContadorDeSenha contadorSenha)
        {
            await _IContadorSenha.Delete(contadorSenha);
        }

        public async Task<List<Domain.Prioridades.Entities.ContadorDeSenha>> FindByCondition(Expression<Func<Domain.Prioridades.Entities.ContadorDeSenha, bool>> expression)
        {
            return await _IContadorSenha.FindByCondition(expression);
        }



        public async Task<Domain.Prioridades.Entities.ContadorDeSenha> GetContadorSenhaById(Guid id, DateTime dtAcesso)
        {
            var lst = await FindByCondition(p => p.SenhaId == id &&
                                         p.DataDeAcesso.Date.Month == dtAcesso.Date.Month &&
                                         p.DataDeAcesso.Date.Year == dtAcesso.Date.Year);

            if (lst != null && lst.Any())
                return lst?.FirstOrDefault();
            else
                return null;

        }
        public async Task<List<Domain.Prioridades.Entities.ContadorDeSenha>> GetContadorSenhaById(Guid id)
        {
            var lst = await FindByCondition(p => p.SenhaId == id);

            if (lst != null && lst.Any())
                return lst;
            else
                return null;

        }

        public async Task<Domain.Prioridades.Entities.ContadorDeSenha> GetEntityById(Guid id)
        {
            return await _IContadorSenha.GetEntityById(id);
        }

        public async Task<List<Domain.Prioridades.Entities.ContadorDeSenha>> List()
        {
            return await _IContadorSenha.List();
        }

        public async Task UpdateSenha(Domain.Prioridades.Entities.ContadorDeSenha contadorSenha)
        {
            await _IContadorSenha.Update(contadorSenha);
        }
    }
}
