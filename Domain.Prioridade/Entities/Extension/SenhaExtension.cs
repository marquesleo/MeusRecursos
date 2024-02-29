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
            dbSenha.Usuario_Site = senhaViewModel.Usuario_Site;
            if (senhaViewModel.ImagemData != null)
                 dbSenha.Imagem = System.Convert.FromBase64String(senhaViewModel.ImagemData.Substring(senhaViewModel.ImagemData.LastIndexOf(',') + 1));
    
            dbSenha.NomeImagem = senhaViewModel.NomeDaImagem;
            dbSenha.Usuario_Id = Guid.Parse(senhaViewModel.Usuario);
            if (!string.IsNullOrEmpty(senhaViewModel.Categoria.ToString()))
            dbSenha.Categoria_Id = Guid.Parse(senhaViewModel.Categoria);
      
        }
    }
}
