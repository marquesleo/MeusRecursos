using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using ApplicationPrioridadesAPP.Interfaces;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade
{
    public class AppPrioridade : InterfacePrioridadeApp
    {
        private readonly IPrioridade _IPrioridade;
        private readonly IUsuario _IUsuario;
        private readonly IServicePrioridade _IServicePrioridade;
        private readonly IServiceUsuario _IServiceUsuario;

        public AppPrioridade(IPrioridade IPrioridade ,
                             IUsuario IUsuario,
                             IServicePrioridade IServicePrioridade,
                             IServiceUsuario IServiceUsuario)
        {
            this._IPrioridade = IPrioridade;
            this._IServicePrioridade = IServicePrioridade;
            this._IUsuario = IUsuario;
            this._IServiceUsuario = IServiceUsuario;
          
        }   
        
       private async Task CarregarUsuario(Domain.Prioridades.Entities.Prioridade prioridade){
         prioridade.Usuario = await _IUsuario.GetEntityById(prioridade.Usuario_Id);
       }
       

        public async Task AddPrioridade(Domain.Prioridades.Entities.Prioridade prioridade)
        {
            await CarregarUsuario(prioridade);
            await _IServicePrioridade.AddPrioridade(prioridade);
        }

        public async Task Delete(Domain.Prioridades.Entities.Prioridade objeto)
        {
            await _IPrioridade.Delete(objeto);
        }

        public async Task<List<PrioridadeViewModel>> ObterPrioridade(string id_usuario,bool? feito=false)
        {

            var lstNovasPrioridades =new List<PrioridadeViewModel>();

            try{

                   var lstPrioridade = await _IPrioridade.FindByCondition(p=> p.Usuario.Id == Guid.Parse(id_usuario) &&
                                                                          p.Feito == feito);
                   if (lstPrioridade != null && lstPrioridade.Any())
                   {
                        foreach(var obj in lstPrioridade){
                            var prioridadeViewModel = new PrioridadeViewModel();
                            prioridadeViewModel.Ativo = obj.Ativo;
                            prioridadeViewModel.Descricao = obj.Descricao;
                            prioridadeViewModel.Feito = obj.Feito;
                            prioridadeViewModel.Id = obj.Id;
                            prioridadeViewModel.Valor = obj.Valor;
                            prioridadeViewModel.Usuario =Guid.Parse(id_usuario);
                            lstNovasPrioridades.Add(prioridadeViewModel);
                        } 

                    }
             }catch(Exception ex){
                  throw ex;
            }
           
            return lstNovasPrioridades.OrderBy(p=> p.Valor).ToList();
        }


        public async Task<List<Domain.Prioridades.Entities.Prioridade>> FindByCondition(Expression<Func<Domain.Prioridades.Entities.Prioridade, bool>> expression)
        {
            return await _IPrioridade.FindByCondition(expression);
        }

        public async Task<Domain.Prioridades.Entities.Prioridade> GetEntityById(Guid id)
        {
            return await _IPrioridade.GetEntityById(id);
        }

        public async Task<List<Domain.Prioridades.Entities.Prioridade>> List()
        {
            return await _IPrioridade.List();
        }
    
        public async Task UpdatePrioridade(Domain.Prioridades.Entities.Prioridade prioridade)
        {
            await CarregarUsuario(prioridade);
            await _IServicePrioridade.UpdatePrioridade(prioridade);
        }

        public async Task Up(Domain.Prioridades.Entities.Prioridade prioridade)
        {
           await _IServicePrioridade.Up(prioridade);
        }

        public async Task Down(Domain.Prioridades.Entities.Prioridade prioridade)
        {
            await _IServicePrioridade.Down(prioridade);
        }

        public async Task SetOrder(Domain.Prioridades.Entities.Prioridade prioridade, enuOrdem ordem)
        {
            await _IServicePrioridade.SetOrder(prioridade,ordem);
        }
    }
}
