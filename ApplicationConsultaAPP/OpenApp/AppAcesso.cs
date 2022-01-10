using ApplicationConsultaAPP.Interfaces;
using Domain.Consulta.Entities;
using Domain.Consulta.Interface;
using Domain.Consulta.Interfaces;
using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.OpenApp
{
    public class AppAcesso : InterfaceAcessoApp
    {

        private readonly IAcesso _IAcesso;
        private readonly IUsuario _IUsuario;
        private readonly IEmpresa _IEmpresa;
        public AppAcesso(IAcesso IAcesso, 
                         IUsuario IUsuario,
                         IEmpresa IEmpresa)
        {
            this._IAcesso = IAcesso;
            this._IUsuario = IUsuario;
            this._IEmpresa = IEmpresa;
        }
        public async Task Incluir(AcessoViewModel acessoViewModel)
        {
            try
            {
                Acesso acesso = await carregarObjeto(acessoViewModel);
                await _IAcesso.Add(acesso);
                acessoViewModel.Id = acesso.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
         }

        private async Task<Acesso> carregarObjeto(AcessoViewModel acessoViewModel)
        {
            var acesso = new Acesso();
            acesso.Empresa_Id = acessoViewModel.Empresa_Id;
            acesso.Usuario_Id = acessoViewModel.Usuario_Id;
            acesso.tipo = acessoViewModel.Tipo;
            acesso.Usuario = await _IUsuario.GetEntityById(acesso.Usuario_Id);
            acesso.Empresa = await _IEmpresa.GetEntityById(acesso.Empresa_Id);
            return acesso;
        }

        public async Task Alterar(AcessoViewModel acessoViewModel)
        {
            try {
                Acesso acesso = await carregarObjeto(acessoViewModel);

                await _IAcesso.Update(acesso);
             
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task Delete(Acesso objeto)
        {
            try
            {
               await  _IAcesso.Delete(objeto);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Acesso>> FindByCondition(Expression<Func<Acesso, bool>> expression)
        {
            try
            {
                return await _IAcesso.FindByCondition(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Acesso> GetEntityById(Guid id)
        {
            try
            {
                return await _IAcesso.GetEntityById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      

        public async Task<List<Acesso>> List()
        {
            try
            {
                return await _IAcesso.List();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Acesso> ObterAcessoPorUsuarioEEmpresa(Guid idUsuario, Guid idEmpresa)
        {
            var Acesso = await FindByCondition(p => p.Usuario_Id == idUsuario && p.Empresa_Id == idEmpresa);
            return Acesso?.FirstOrDefault();
        }

        public async Task<Acesso> ObterAcessoPorUsuario(Guid idUsuario)
        {
            var Acesso = await FindByCondition(p => p.Usuario_Id == idUsuario);
            return Acesso?.FirstOrDefault();
        }
    }
}
