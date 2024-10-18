using Contracts.Generics;
using DInfrastructure.Repository.Interface;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Win32.SafeHandles;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generics
{
    public class RepositoryGeneric<T> : IGeneric<T>,  IDisposable where T : class
    {
        protected MyDB myDB;
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        public RepositoryGeneric(MyDB myDB) {
            this.myDB = myDB;
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        private IDbContextTransaction _transacao;
        public async Task BeginTransaction()
        {
            var data = new ContextBase(_optionsBuilder, myDB);
            _transacao = await data.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            if (_transacao != null)
            {
                await _transacao.CommitAsync();
                _transacao = null;
            }
        }

        public async Task Rollback()
        {
            if (_transacao != null)
                await _transacao.RollbackAsync();
        }

        public async Task Add(T objeto)
        {
            try
            {
                using (var data = new ContextBase(_optionsBuilder, myDB))
                {
                   
                    await data.Set<T>().AddAsync(objeto);
                    await data.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
         
        }

        public async Task Delete(T objeto)
        {
            try
            {
                using (var data = new ContextBase(_optionsBuilder, myDB))
                {
                     data.Set<T>().Remove(objeto);
                    await data.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression)
        {

            using (var data = new ContextBase(_optionsBuilder, myDB))
            {
                return  await data.Set<T>().Where(expression).AsNoTracking().ToListAsync();
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
            try
            {
                using (var data = new ContextBase(_optionsBuilder, myDB))
                {
                    
                    data.Set<T>().Update(objeto);
                    await data.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
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

       



        #endregion

    }
}
