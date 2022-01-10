using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceServices;
using Domain.Consulta.Validations;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Services
{
    public class ServiceEmpresa : BaseService, IServiceEmpresa
    {
        private readonly IEmpresa _IEmpresa;

        public ServiceEmpresa(IEmpresa IEmpresa,
                                  INotificador notificador) : base(notificador)
        {
            this._IEmpresa = IEmpresa;
        }

        public async Task AddEmpresa(Empresa empresa)
        {
            if (!ExecutarValidacao(new EmpresaValidation(), empresa)) return;

            await _IEmpresa.Add(empresa);
        }

        public async Task AlterarEmpresa(Empresa empresa)
        {
            if (!ExecutarValidacao(new EmpresaValidation(), empresa)) return;

            await _IEmpresa.Update(empresa);
        }

        public async Task<Empresa> ObterEmpresa(string id)
        {
            return await _IEmpresa.GetEntityById(Guid.Parse(id));
        }

        public async Task<List<Empresa>> ObterEmpresas(string nomeCompleto)
        {
            return await _IEmpresa.FindByCondition(p=> p.Nome.Contains(nomeCompleto));
        }
    }
}
