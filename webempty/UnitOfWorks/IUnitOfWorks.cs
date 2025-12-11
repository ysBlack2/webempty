using Microsoft.EntityFrameworkCore.Storage;
using webempty.SharedRepositories;

namespace webempty.UnitOfWorks
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<T> Repository<T>() where T : class;
		public Task<IDbContextTransaction> BeginTransactionAsync();
		public Task CommitTransactionAsync();
		public Task RollbackTransactionAsync();
		Task<int> CompleteAsync();
	}
}
