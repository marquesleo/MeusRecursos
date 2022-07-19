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

        public async Task AddSenha(Senha senha)
        {
            await _IServiceSenha.AddSenha(senha);
        }

        public async Task Delete(Senha objeto)
        {
            await _ISenha.Delete(objeto);
        }
              
        public async Task<List<Senha>> FindByCondition(Expression<Func<Senha, bool>> expression)
        {
            return await _ISenha.FindByCondition(expression);
        }

        public async Task<Senha> GetEntityById(Guid id)
        {
            return await _ISenha.GetEntityById(id);
        }

        public async Task<List<Senha>> List()
        {
            return await _ISenha.List();
        }

        public async Task<List<SenhaViewModel>> ObterRegistros(string id_usuario)
        {
            var lstSenhas = await _ISenha.FindByCondition(p => p.Usuario.Id == Guid.Parse( id_usuario));
            var lstSenhaViewModel = new List<SenhaViewModel>();
            if (lstSenhas !=null && lstSenhas.Any())
            {
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

            return  lstSenhaViewModel;

        }

        private async Task CarregarUsuario(Senha senha)
        {
            senha.Usuario = await _IUsuario.GetEntityById(senha.Usuario.Id);
         
        }

        public async Task UpdateSenha(Senha senha)
        {
            await CarregarUsuario(senha);
            await _IServiceSenha.UpdateSenha(senha);
        }
    }
}
