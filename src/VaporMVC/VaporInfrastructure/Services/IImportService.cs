using VaporDomain.Model;

namespace VaporInfrastructure.Services
{
    public interface IImportService<TEntity>
    where TEntity : Entity
    {
        Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}