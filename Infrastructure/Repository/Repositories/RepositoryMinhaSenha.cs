using Domain.Interface;
using Entities.Models;
using Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryMinhaSenha : Generics.RepositoryGeneric<MinhaSenha>, IMinhaSenha
    {
        public RepositoryMinhaSenha(MyDB myDB) : base(myDB) { }
    }
}
