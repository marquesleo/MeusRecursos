using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Prioridades.Entities.Extension
{
    public static class SenhaExtension
   {
        public static void Map(this Senha dbSenha, ViewModels.SenhaViewModel  senhaViewModel)
        {
            dbSenha.Descricao = senhaViewModel.Descricao;
            dbSenha.Ativo = senhaViewModel.Ativo;
            dbSenha.Id = senhaViewModel.Id;
            dbSenha.DtAtualizacao = senhaViewModel.DtAtualizacao;
            dbSenha.Observacao = senhaViewModel.Observacao;
            dbSenha.Password = senhaViewModel.Password;
            dbSenha.UrlImageSite = senhaViewModel.UrlImageSite;
            dbSenha.Site = senhaViewModel.Site;
            dbSenha.Usuario = new Usuario();
            dbSenha.Usuario.Id = Guid.Parse(senhaViewModel.Usuario);
      
        }
    }
}
