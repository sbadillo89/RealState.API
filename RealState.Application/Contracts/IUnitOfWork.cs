using RealState.Application.Contracts.Repositories;

namespace RealState.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IRealStateRepository RealStateRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
