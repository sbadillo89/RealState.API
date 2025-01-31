using RealState.Application.Contracts;
using RealState.Application.Contracts.Repositories;
using RealState.Infrastructure.Persistence;
using RealState.Infrastructure.Repositories;

namespace RealState.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public IRealStateRepository RealStateRepository { get; }
    private RealStateContext _context;

    public UnitOfWork(RealStateContext context)
    {
        _context = context;
        RealStateRepository = new RealStateRepository(_context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = _context.SaveChanges();
        // return Convert.ToBoolean(result);
        return result;
    }

    public void Dispose()
    {
        // _context.Dispose();
    }
}