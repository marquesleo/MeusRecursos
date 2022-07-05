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

namespace ApplicationPrioridadesAPP.OpenApp
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
        
       private async Task CarregarUsuario(Prioridade prioridade){
         prioridade.Usuario = await _IUsuario.GetEntityById(prioridade.Usuario.Id);
       }
       

        public async Task AddPrioridade(Prioridade prioridade)
        {
            await CarregarUsuario(prioridade);
            await _IServicePrioridade.AddPrioridade(prioridade);
        }

        public async Task Delete(Prioridade objeto)
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
                            prioridadeViewModel.Usuario =id_usuario;
                            lstNovasPrioridades.Add(prioridadeViewModel);
                        } 

                    }
             }catch(Exception ex){
                  throw ex;
            }
           
            return lstNovasPrioridades.OrderBy(p=> p.Valor).ToList();
        }


        public async Task<List<Prioridade>> FindByCondition(Expression<Func<Prioridade, bool>> expression)
        {
            return await _IPrioridade.FindByCondition(expression);
        }

        public async Task<Prioridade> GetEntityById(Guid id)
        {
            return await _IPrioridade.GetEntityById(id);
        }

        public async Task<List<Prioridade>> List()
        {
            return await _IPrioridade.List();
        }
    
        public async Task UpdatePrioridade(Prioridade prioridade)
        {
            await CarregarUsuario(prioridade);
            await _IServicePrioridade.UpdatePrioridade(prioridade);
        }

        public async Task Up(Prioridade prioridade)
        {
           await _IServicePrioridade.Up(prioridade);
        }

        public async Task Down(Prioridade prioridade)
        {
            await _IServicePrioridade.Down(prioridade);
        }
    }
}
