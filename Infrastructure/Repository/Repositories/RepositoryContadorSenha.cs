using System;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interfaces;
using Infrastructure.Configuration;

namespace Infrastructure.Repository.Repositories
{
	public class RepositoryContadorSenha : Generics.RepositoryGeneric<ContadorDeSenha>, IContadorSenha
    {
        public RepositoryContadorSenha(MyDB myDB) : base(myDB) { }
    }
}

