using MediatR;
using TinyUrl.Application.Common.Interfaces.Repositories;
using TinyUrl.Domain;

namespace TinyUrl.Application.Queries.RedirectFromShortenedUrl;

public class RedirectFromShortenedUrlQueryHandler(IShortenedRepository shortenedRepository)
    : IRequestHandler<RedirectFromShortenedUrlQuery, ShortenedUrl>
{

    private readonly IShortenedRepository _shortenedRepository = shortenedRepository;

    public async Task<ShortenedUrl> Handle(
        RedirectFromShortenedUrlQuery query,
        CancellationToken cancellationToken)
    {
        var shortenedUrl = await _shortenedRepository.GetAsync(
            url => url.ShortUrl.Contains(query.ShortenedUrl),
            cancellationToken);

        if (shortenedUrl is null)
        {
            return default!;
        }

        shortenedUrl.IncreaseTimesUsed();

        await _shortenedRepository.PatchAsync(shortenedUrl, cancellationToken);

        return shortenedUrl;
    }
}
