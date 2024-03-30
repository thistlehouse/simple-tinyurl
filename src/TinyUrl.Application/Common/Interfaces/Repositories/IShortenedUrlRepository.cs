using System.Linq.Expressions;
using TinyUrl.Domain;

namespace TinyUrl.Application.Common.Interfaces.Repositories;

public interface IShortenedRepository
{
    Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken);
    Task<ShortenedUrl> GetAsync(
        Expression<Func<ShortenedUrl, bool>> filter,
        CancellationToken cancellationToken);

    Task PatchAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken);
}