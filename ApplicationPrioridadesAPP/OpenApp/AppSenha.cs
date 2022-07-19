using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.ViewModels;

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppSenha : InterfaceSenhaApp
    {

        private readonly ISenha _ISenha;
        private readonly IUsuario _IUsuario;
        private readonly IServiceSenha _IServiceSenha;
        

        public AppSenha(ISenha ISenha,
                        IUsuario IUsuario,
                        IServiceSenha IServiceSenha)
        {
            this._ISenha = ISenha;
            this._IUsuario = IUsuario;
            this._IServiceSenha = IServiceSenha;
        }

        public async Task AddSenha(SenhaViewModel senha)
        {
            try{
                var senhaNova = new Senha();
                senhaNova.Map(senha);
                await _IServiceSenha.AddSenha(senhaNova);
            } catch(Exception ex){
                  throw ex;  
            }
           
        }

        public async Task Delete(Senha objeto)
        {
            try{ 
                await _ISenha.Delete(objeto);
            }catch(Exception ex){
                throw ex;
            }
        }
              
        public async Task<List<Senha>> FindByCondition(Expression<Func<Senha, bool>> expression)
        {
            try{
                return await _ISenha.FindByCondition(expression);
            }catch(Exception ex){
                 throw ex;
            }  
            
        }

        public async Task<Senha> GetEntityById(Guid id)
        {
            try
            { 
              return await _ISenha.GetEntityById(id);
            } catch(Exception ex){
                 throw ex;
            }  
        }

        public async Task<List<Senha>> List()
        {
            try{
                return await _ISenha.List();
            }catch(Exception ex){
                 throw ex;
            }
        }

        public async Task<List<SenhaViewModel>> ObterRegistros(string id_usuario)
        {
            var lstSenhas = await _ISenha.FindByCondition(p => p.Usuario.Id == Guid.Parse( id_usuario));
            var lstSenhaViewModel = new List<SenhaViewModel>();
            try
            {            
             if (lstSenhas !=null && lstSenhas.Any())
                if (lstSenhas != null && lstSenhas.Any())
                {
                    foreach (var obj in lstSenhas)
                    {
                        var senhaViewModel = new SenhaViewModel();
                        senhaViewModel.Ativo = obj.Ativo;
                        senhaViewModel.Descricao = obj.Descricao;
                        senhaViewModel.Site = obj.Site;
                        senhaViewModel.Id = obj.Id;
                        senhaViewModel.Observacao = obj.Observacao;
                        senhaViewModel.Usuario =id_usuario;
                        senhaViewModel.DtAtualizacao = obj.DtAtualizacao;
                        senhaViewModel.Password = obj.Password;
                        senhaViewModel.UrlImageSite = obj.UrlImageSite;
                       
                        lstSenhaViewModel.Add(senhaViewModel);
                    }

                }
            }
            
            catch (Exception ex) 
            {
                
                throw ex;
            }
            return  lstSenhaViewModel;
        }
        
        private async Task CarregarUsuario(Senha senha)
        {
            try
            {

                 senha.Usuario = await _IUsuario.GetEntityById(senha.Usuario.Id);
         
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        public async Task UpdateSenha(SenhaViewModel senha)
        {
            try
            {
                var  senhaNova = new Senha();
                senhaNova.Map(senha);
                 await CarregarUsuario(senhaNova);
                 await _IServiceSenha.UpdateSenha(senhaNova);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
    }
}
