using System;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.Interfaces;
using Infrastructure.Configuration;

namespace Infrastructure.Repository.Repositories
{
	public class RepositoryCategoria : Generics.RepositoryGeneric<Categoria>, ICategoria
    {
        public RepositoryCategoria(MyDB myDB) : base(myDB) { }
    }
}

