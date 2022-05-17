using Contracts.Generics;
using Infrastructure.Consulta.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace Infrastructure.Consulta.Repository.Generics
{
    public abstract class RepositoryGeneric<T> : IGeneric<T>, IDisposable where T : class
    {
        protected MyDB myDB;
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositoryGeneric(MyDB myDB) {
            this.myDB = myDB;
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }
        public async Task Add(T objeto)
        {
            using(var data = new ContextBase(_optionsBuilder, myDB))
            {
                await data.Set<T>().AddAsync(objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T objeto)
        {
            using (var data = new ContextBase(_optionsBuilder, myDB))
            {
                data.Set<T>().Remove(objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {

            using (var data = new ContextBase(_optionsBuilder, myDB))
            {
                return  await data.Set<T>().Where(expression).ToListAsync();
            }
            
        }

        public async Task<T> GetEntityById(Guid id)
        {
            using (var data = new ContextBase(_optionsBuilder, myDB))
            {
                return await data.Set<T>().FindAsync(id);
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_optionsBuilder, myDB))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task Update(T objeto)
        {
            using (var data = new ContextBase(_optionsBuilder, myDB))
            {
                 data.Set<T>().Update(objeto);
                await data.SaveChangesAsync();
            }
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);



        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }


        protected virtual async Task<List<object>> RetornarParametro(T Objeto) {

            return null;
        }
            
        
        public virtual async Task Exec(String sql,T Objeto)
        {
           using (var data = new ContextBase(_optionsBuilder, myDB))
            {
                 data.Set<T>().FromSqlRaw(sql, await RetornarParametro(Objeto));
                await data.SaveChangesAsync();
            }
        }
        #endregion

    }
}
