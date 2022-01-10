using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceServices;
using Domain.Consulta.Validations;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Services
{
    public class ServiceConsulta : BaseService, IServiceConsulta
    {
        private readonly IConsulta _IConsulta;

        public ServiceConsulta(IConsulta IConsulta,
                                  INotificador notificador) : base(notificador)
        {
            _IConsulta = IConsulta;
        }

        public async Task AddConsulta(Entities.Consulta consulta)
        {
            try
            {
                if (!ExecutarValidacao(new ConsultaValidation(), consulta)) return;
                await _IConsulta.Add(consulta);
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public  async Task AlterarConsulta(Entities.Consulta consulta)
        {
            try
            {
                if (!ExecutarValidacao(new ConsultaValidation(), consulta)) return;
                await _IConsulta.Update(consulta);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Entities.Consulta>> ConsultarAgenda(Guid empresa_id, DateTime diaInicial, DateTime diaFinal, Guid paciente_id, Guid psicologo_id)
        {
            try
            {
                return await _IConsulta.FindByCondition(p => p.Psicologa.Acesso.Empresa_Id == empresa_id &&
                                                       diaInicial.Year > 1900 && diaFinal.Year > 1900 ?
                                                        p.Horario >= diaInicial && p.Horario <= diaFinal :
                                                        p.Horario >= DateTime.MinValue && p.Horario <= DateTime.MaxValue &&
                                                       (paciente_id == Guid.Empty || p.Paciente_Id == paciente_id) && 
                                                       (psicologo_id == Guid.Empty || p.Psicologa_Id == psicologo_id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Entities.Consulta>> ConsultarAgendaNaoRealizada(Guid empresa_id, string dia, string paciente, string psicologa)
        {
            try
            {

                if (string.IsNullOrEmpty(paciente))
                    paciente = string.Empty;

                if (string.IsNullOrEmpty(psicologa))
                    psicologa = string.Empty;

                DateTime dtFiltro = Convert.ToDateTime(dia);
                
               return await _IConsulta.FindByCondition(p => p.Psicologa.Acesso.Empresa_Id == empresa_id  && p.Status.ToUpper() == "N" &&
                                                                (dtFiltro.Year > 1900  ?
                                                                  p.Horario.Date == dtFiltro.Date :  p.Horario.Date == DateTime.Now.Date) && 
                                                               (paciente.Equals("") || p.Paciente.Nome.ToUpper().Contains(paciente.ToUpper())) &&
                                                               (psicologa.Equals("") || p.Psicologa.Nome.ToUpper().Contains(psicologa)));

              
                 
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task Delete(Entities.Consulta consulta)
        {
            try
            {
               await  _IConsulta.Delete(consulta);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Entities.Consulta>> FindByCondition(Expression<Func<Entities.Consulta, bool>> expression)
        {
            try
            {
                return await _IConsulta.FindByCondition(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Entities.Consulta> GetEntityById(Guid id)
        {
            try
            {
                return await _IConsulta.GetEntityById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> IsJaExisteMarcado(Guid empresa_id, DateTime diaHora)
        {
            try
            {
                return await _IConsulta.IsJaExisteMarcado(empresa_id, diaHora);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> IsJaExisteMarcado(Guid empresa_id, Guid consulta_id, DateTime diaHora)
        {
            try
            {
                return await _IConsulta.IsJaExisteMarcado(empresa_id, consulta_id, diaHora);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Entities.Consulta>> List()
        {
            try
            {
                return await _IConsulta.List();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
