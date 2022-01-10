using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceServices;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Services
{
    public class ServicePsicologa : BaseService, IServicePsicologa
    {
        private readonly IPsicologa _IPsicologa;

        public ServicePsicologa(IPsicologa IPsicologa,
                                  INotificador notificador) : base(notificador)
        {
            this._IPsicologa = IPsicologa;
        }

        public async Task AddPsicologa(Psicologa psicologa)
        {
            try
            {
                await _IPsicologa.Add(psicologa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task AlterarPsicologa(Psicologa psicologa)
        {
            try
            {
                await _IPsicologa.Update(psicologa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Psicologa>> ObterPsicologos(Guid Empresa_Id)
        {
            try
            {
                return await _IPsicologa.ObterPsicologos(Empresa_Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Psicologa> ObterPsicologa(string id)
        {
            try
            {
                return await _IPsicologa.GetEntityById(Guid.Parse(id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
