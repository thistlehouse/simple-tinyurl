using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TinyUrl.Application.Common.Interfaces.Repositories;
using TinyUrl.Domain;
using TinyUrl.Infrastructure.Common.Settings;

namespace TinyUrl.Infrastructure.Repositories;

public class ShortenedUrlRepository
    : IShortenedRepository
{
    private readonly IMongoCollection<ShortenedUrl> _entityCollection;

    public ShortenedUrlRepository(IOptions<TinyUrlDbSettings> options)
    {
        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.Database);

        _entityCollection = database.GetCollection<ShortenedUrl>(options.Value.Collection);
    }

    public async Task AddAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
    {
        await _entityCollection.InsertOneAsync(
            shortenedUrl,
            cancellationToken: cancellationToken);
    }

    public async Task<ShortenedUrl> GetAsync(
        Expression<Func<ShortenedUrl, bool>> filter,
        CancellationToken cancellationToken)
    {
        return await _entityCollection.Find(filter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task PatchAsync(ShortenedUrl shortenedUrl, CancellationToken cancellationToken)
    {
        await _entityCollection.ReplaceOneAsync(
            url => url.Id == shortenedUrl.Id,
            shortenedUrl,
            cancellationToken: cancellationToken);
    }
}
