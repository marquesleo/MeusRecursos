using ApplicationConsultaAPP.Interfaces;
using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceServices;
using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.OpenApp
{
    public class AppPsicologa : InterfacePsicologaApp
    {
        private readonly IPsicologa _IPsicologa;
        private readonly IServicePsicologa _servicePsicologa;
        private readonly IAcesso _IAcesso;
        public AppPsicologa(IPsicologa IPsicologa,
                            IServicePsicologa ServicePsicologa,
                           IAcesso IAcesso)
        {
            _IPsicologa = IPsicologa;
            _servicePsicologa = ServicePsicologa;
            _IAcesso = IAcesso;
        }

        public async Task Alterar(Psicologa psicologa)
        {
            try
            {
               await _servicePsicologa.AlterarPsicologa(psicologa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Delete(Psicologa objeto)
        {
            try
            {
                await _IPsicologa.Delete(objeto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Psicologa>> FindByCondition(Expression<Func<Psicologa, bool>> expression)
        {
            try
            {
                return await _IPsicologa.FindByCondition(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Psicologa> GetEntityById(Guid id)
        {
            try
            {
                return await _servicePsicologa.ObterPsicologa(id.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task Incluir(PsicologaViewModel psicologaViewModel)
        {
            try
            {
                var psicologa = new Psicologa();
                psicologa.Ativo = psicologaViewModel.Ativo;
                psicologa.Bairro = psicologaViewModel.Bairro;
                psicologa.Celular = psicologaViewModel.Celular;
                psicologa.Telefone = psicologaViewModel.Telefone;
                psicologa.Nome = psicologaViewModel.Nome;
                psicologa.Cep = psicologaViewModel.Cep;
                psicologa.Cidade = psicologaViewModel.Cidade;
                psicologa.CRP = psicologaViewModel.CRP;
                psicologa.DtNascimento = psicologaViewModel.DtNascimento;
                psicologa.Email = psicologaViewModel.Email;
                psicologa.Endereco = psicologaViewModel.Endereco;
                psicologa.Cidade = psicologaViewModel.Cidade;
                psicologa.Acesso_Id = psicologaViewModel.Acesso_Id;

                await _servicePsicologa.AddPsicologa(psicologa);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsPacienteExiste(string nome, string email, string telefone)
        {
            try
            {
                var psicologa = FindByCondition(p => p.Nome.ToUpper() == nome.ToUpper() ||
                                                            p.Email.ToUpper() == email ||
                                                            p.Telefone == telefone).Result;

                if (psicologa != null && psicologa.Any())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Psicologa>> List()
        {
            try
            {
                return await _IPsicologa.List();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<List<Psicologa>> ObterPorNome(string nome)
        {
            try
            {
                if (string.IsNullOrEmpty(nome))
                    nome = "";
                return await _IPsicologa.FindByCondition(p => p.Nome.ToUpper().Contains(nome.ToUpper()));

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public  async Task<List<Psicologa>> ObterPsicologos(Guid Empresa_Id)
        {
            return await _servicePsicologa.ObterPsicologos(Empresa_Id);
        }
    }
}
