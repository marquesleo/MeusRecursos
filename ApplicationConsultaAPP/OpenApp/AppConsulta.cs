using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceService;
using Domain.Consulta.InterfaceServices;
using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.OpenApp
{
    public class AppConsulta : InterfaceConsultaApp
    {
        private readonly IServiceConsulta _IServiceConsulta;
  
        private readonly IMapper _mapper;

        public AppConsulta(IServiceConsulta IServiceConsulta,
                           
                           IMapper mapper)
        {
            _IServiceConsulta = IServiceConsulta;
          
            _mapper = mapper;
           
        }

        public async Task AddConsulta(ConsultaViewModel consulta)
        {
            try
            {
                Consulta objeto = _mapper.Map<Consulta>(consulta);
                await _IServiceConsulta.AddConsulta(objeto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task AlterarConsulta(ConsultaViewModel consulta)
        {
            try
            {
                Consulta objeto = _mapper.Map<Consulta>(consulta);
                await _IServiceConsulta.AlterarConsulta(objeto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Consulta>> ConsultarAgenda(Guid empresa_id, DateTime diaInicial, DateTime diaFinal, Guid paciente_id, Guid psicologo_id)
        {
            try
            {
              return   await _IServiceConsulta.ConsultarAgenda(empresa_id,diaInicial,diaFinal,paciente_id,psicologo_id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Consulta>> ConsultarAgendaNaoRealizada(Guid empresa_id, string dia, string paciente, string psicologa)
        {
            try
            {
                return await _IServiceConsulta.ConsultarAgendaNaoRealizada(empresa_id, dia, paciente, psicologa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Delete(Consulta objeto)
        {
            try
            {
                await _IServiceConsulta.Delete(objeto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Consulta>> FindByCondition(Expression<Func<Consulta, bool>> expression)
        {
            try
            {
                return await _IServiceConsulta.FindByCondition(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Consulta> GetEntityById(Guid id)
        {
            try
            {
                return await _IServiceConsulta.GetEntityById(id);
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
                return await _IServiceConsulta.IsJaExisteMarcado(empresa_id, diaHora);
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
                return await _IServiceConsulta.IsJaExisteMarcado(empresa_id,consulta_id, diaHora);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Consulta>> List()
        {
            try
            {
                return await _IServiceConsulta.List();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
