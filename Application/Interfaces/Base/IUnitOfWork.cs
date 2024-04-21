namespace Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> Save(CancellationToken cancellationToken);
}