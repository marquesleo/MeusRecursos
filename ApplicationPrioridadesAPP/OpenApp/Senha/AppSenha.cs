using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.Interface;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.ViewModels;

namespace ApplicationPrioridadesAPP.OpenApp.Senha
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
            _ISenha = ISenha;
            _IUsuario = IUsuario;
            _IServiceSenha = IServiceSenha;
        }



        public async Task AddSenha(Domain.Prioridades.Entities.Senha senha)
        {
            try
            {


              //  senha.Password = Utils.Criptografia.CriptografarSenha(senha.Password);
                if (senha.Categoria_Id == Guid.Empty)
                    senha.Categoria_Id = null;
                await _IServiceSenha.AddSenha(senha);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task Delete(Domain.Prioridades.Entities.Senha objeto)
        {
            try
            {
                await _ISenha.Delete(objeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Domain.Prioridades.Entities.Senha>> FindByCondition(Expression<Func<Domain.Prioridades.Entities.Senha, bool>> expression)
        {
            try
            {
                return await _ISenha.FindByCondition(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Domain.Prioridades.Entities.Senha> GetSenhaById(string id)
        {
            try
            {
                var senha = await GetEntityById(Guid.Parse(id));
                return senha;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Domain.Prioridades.Entities.Senha> GetEntityById(Guid id)
        {
            try
            {
                return await _ISenha.GetEntityById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Domain.Prioridades.Entities.Senha>> List()
        {
            try
            {
                return await _ISenha.List();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private SenhaViewModel SenhaEntityToSenhaViewModel(Domain.Prioridades.Entities.Senha senha)
        {
            var senhaViewModel = new SenhaViewModel();
            senhaViewModel.Ativo = senha.Ativo;
            senhaViewModel.Descricao = senha.Descricao;
            senhaViewModel.Site = senha.Site;
            senhaViewModel.Id = senha.Id;
            senhaViewModel.Observacao = senha.Observacao;
            senhaViewModel.Usuario = senha.Usuario_Id.ToString();
            senhaViewModel.DtAtualizacao = senha.DtAtualizacao;
           // senhaViewModel.Password = Utils.Criptografia.Decriptografar(senha.Password);
            senhaViewModel.UrlImageSite = senha.UrlImageSite;
            senhaViewModel.Usuario_Site = senha.Usuario_Site;
            if (senha.Categoria_Id != null && senha.Categoria_Id.HasValue)
                senhaViewModel.Categoria = senha.Categoria_Id.Value.ToString();
            if (senha.Imagem != null && senha.Imagem.Length > 0)
            {
                string base64String = Convert.ToBase64String(senha.Imagem, 0, senha.Imagem.Length);
                senhaViewModel.ImagemData = "data:image/png;base64," + base64String;
            }
            return senhaViewModel;
        }
        public async Task<List<Domain.Prioridades.Entities.Senha>> ObterRegistros(string id_usuario)
        {
            var lstSenhas = await _ISenha.FindByCondition(p => p.Usuario.Id == Guid.Parse(id_usuario));
        
            return lstSenhas;
        }

        private async Task CarregarUsuario(Domain.Prioridades.Entities.Senha senha)
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

        public async Task UpdateSenha(Domain.Prioridades.Entities.Senha senha)
        {
            try
            {
                await CarregarUsuario(senha);
                senha.DtAtualizacao = DateTime.Now;
                //senha.Password = Utils.Criptografia.CriptografarSenha(senha.Password);
                if (senha.Categoria_Id == Guid.Empty)
                    senha.Categoria_Id = null;
                await _IServiceSenha.UpdateSenha(senha);
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
                var senhas = await List();

                await _ISenha.BeginTransaction();
                foreach (var item in senhas)
                {
                    item.Password = Utils.Criptografia.CriptografarSenha(item.Password);
                    await _ISenha.Update(item);
                }
                await _ISenha.Commit();



            }
            catch (Exception ex)
            {
                await _ISenha.Rollback();
                throw;
                
            }

            return true;
        }

        public async Task<List<SenhaViewModel>> ObterRegistros(string id_usuario, string descricao)
        {
            var lstSenhas = await _ISenha.FindByCondition(p => p.Usuario.Id == Guid.Parse(id_usuario) &&
                                                         (p.Descricao.ToLower().Contains(descricao.ToLower()) ||
                                                          p.Observacao.ToLower().Contains(descricao.ToLower()) ||
                                                          p.Site.ToLower().Contains(descricao.ToLower())));
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

        public async Task<List<Domain.Prioridades.Entities.Senha>> ObterRegistrosPorFiltros(string id_usuario, string descricao)
        {
            try
            {
                var lstSenhas = await _ISenha.FindByCondition(p => p.Usuario.Id == Guid.Parse(id_usuario) &&
                                                        ((!string.IsNullOrEmpty(descricao) &&  
                                                        descricao.Contains(p.Descricao.ToLower() + p.Observacao.ToLower() + p.Site.ToLower()))));


                return lstSenhas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }
    }
}
