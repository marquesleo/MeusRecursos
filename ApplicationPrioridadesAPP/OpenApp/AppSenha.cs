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
                senhaNova.Password = Utils.Criptografia.CriptografarSenha(senhaNova.Password);
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

        public async Task<SenhaViewModel> GetSenhaById(string id)
        {
            try
            {
                var senha = await GetEntityById(Guid.Parse(id));
                var senhaViewModel = SenhaEntityToSenhaViewModel(senha);
                return senhaViewModel;
               
            }
            catch(Exception e)
            {
                throw e;                
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

        private SenhaViewModel SenhaEntityToSenhaViewModel(Senha senha)
        {
                       var senhaViewModel = new SenhaViewModel();
                        senhaViewModel.Ativo = senha.Ativo;
                        senhaViewModel.Descricao = senha.Descricao;
                        senhaViewModel.Site = senha.Site;
                        senhaViewModel.Id = senha.Id;
                        senhaViewModel.Observacao = senha.Observacao;
                        senhaViewModel.Usuario =  senha.Usuario_Id.ToString();
                        senhaViewModel.DtAtualizacao = senha.DtAtualizacao;
                        senhaViewModel.Password = Utils.Criptografia.Decriptografar(senha.Password);
                        senhaViewModel.UrlImageSite = senha.UrlImageSite;
                        senhaViewModel.Usuario_Site = senha.Usuario_Site;
                        if (senha.Categoria_Id != null && senha.Categoria_Id.HasValue)
                        senhaViewModel.Categoria =  senha.Categoria_Id.Value.ToString();
                        if (senha.Imagem != null && senha.Imagem.Length > 0)
                        {
                          string base64String = Convert.ToBase64String(senha.Imagem, 0, senha.Imagem.Length);
                          senhaViewModel.ImagemData = "data:image/png;base64," + base64String;
                        }
                        return senhaViewModel;
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
                        var senhaViewModel = SenhaEntityToSenhaViewModel(obj);
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

                 senha.Usuario = await _IUsuario.GetEntityById(senha.Usuario_Id);
         
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
                 senhaNova.DtAtualizacao = DateTime.Now;
                 senhaNova.Password = Utils.Criptografia.CriptografarSenha(senhaNova.Password);
                await _IServiceSenha.UpdateSenha(senhaNova);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        public async Task<bool> CriptografarTudo()
        {
            try
            {
                var senhas  = await this.List();

                await _ISenha.BeginTransaction();
                foreach (var item in senhas)
                {
                    item.Password = Utils.Criptografia.CriptografarSenha(item.Password);
                    await _ISenha.Update(item);
                }
                await _ISenha.Commit();



            }catch(Exception ex)
            {
                await _ISenha.Rollback();
                throw ex;
                return false;
            }

            return true;
        }

        public async Task<List<SenhaViewModel>> ObterRegistros(string id_usuario, string descricao)
        {
            var lstSenhas = await _ISenha.FindByCondition(p => p.Usuario.Id == Guid.Parse(id_usuario) &&
                                                          p.Descricao.ToLower().Contains(descricao.ToLower()));
            var lstSenhaViewModel = new List<SenhaViewModel>();
            try
            {
                if (lstSenhas != null && lstSenhas.Any())
                    if (lstSenhas != null && lstSenhas.Any())
                    {
                        foreach (var obj in lstSenhas)
                        {
                            var senhaViewModel = SenhaEntityToSenhaViewModel(obj);
                            lstSenhaViewModel.Add(senhaViewModel);
                        }

                    }
            }

            catch (Exception ex)
            {

                throw ex;
            }
            return lstSenhaViewModel;
        }
    }
}
