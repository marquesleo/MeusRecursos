using System;
using System.Threading.Tasks;

namespace DInfrastructure.Repository.Interface
{
	public interface IUnitOfWork : IDisposable
	{
		Task BeginTransaction();
		Task Commit();
		Task Rollback();
	}
}

