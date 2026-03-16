namespace IntGames.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<Result> SaveChangesAsync(CancellationToken cancellationToken = default);
}
