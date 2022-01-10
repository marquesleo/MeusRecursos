using ApplicationConsultaAPP.Interfaces;
using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.OpenApp
{
    public class AppEmpresa : InterfaceEmpresaApp
    {

        private readonly IEmpresa _IEmpresa;

        public AppEmpresa(IEmpresa IEmpresa)
        {
            this._IEmpresa = IEmpresa;
        }

        public async Task Alterar(Empresa empresa)
        {
            try
            {
                await _IEmpresa.Update(empresa);

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task Delete(Empresa objeto)
        {
            try
            {
                await _IEmpresa.Delete(objeto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        }

        public async Task<List<Empresa>> FindByCondition(Expression<Func<Empresa, bool>> expression)
        {
            try
            {
                return await _IEmpresa.FindByCondition(expression);

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<Empresa> GetEntityById(Guid id)
        {
            try
            {
                return await _IEmpresa.GetEntityById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task Incluir(EmpresaViewModel empresaViewModel)
        {
            try
            {
                var empresa = new Empresa();
                empresa.Nome = empresaViewModel.Nome;
                empresa.Telefone = empresaViewModel.Telefone;
                empresa.Celular = empresaViewModel.Celular;
                empresa.Cep = empresaViewModel.Cep;
                empresa.Cidade = empresaViewModel.Cidade;
                empresa.cnpj = empresaViewModel.Cnpj;
                empresa.Endereco = empresaViewModel.Endereco;
                empresa.Bairro = empresaViewModel.Bairro;
                await _IEmpresa.Add(empresa);
                empresaViewModel.Id = empresa.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsEmpresaExiste(string cnpj)
        {
            var retorno = false;
            try
            {
                var empresa = FindByCondition(p => p.cnpj == cnpj).Result;
                if (empresa != null && empresa.Count > 0)
                    retorno = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return retorno;
        }

        public async Task<List<Empresa>> List()
        {
            try
            {
                return await _IEmpresa.List();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

      
    }
}
