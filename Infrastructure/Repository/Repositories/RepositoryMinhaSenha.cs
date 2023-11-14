using Domain.Interface;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Entities.Models;
using Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryMinhaSenha : Generics.RepositoryGeneric<Senha>, ISenha
    {
        public RepositoryMinhaSenha(MyDB myDB) : base(myDB) { }

       
    }
}
