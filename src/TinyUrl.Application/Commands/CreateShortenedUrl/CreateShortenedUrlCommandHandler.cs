using MediatR;
using TinyUrl.Application.Common.Interfaces.Repositories;
using TinyUrl.Application.Common.Interfaces.Services;
using TinyUrl.Domain;

namespace TinyUrl.Application.Commands.ShortUrl;

public class CreateShortenedUrlCommandHandler(
    IUrlGenerator urlGenerator,
    IShortenedRepository shortenedRepository)
    : IRequestHandler<CreateShortenedUrlCommand, ShortenedUrl>
{
    private readonly IUrlGenerator _urlGenerator = urlGenerator;
    private readonly IShortenedRepository _shortenerRepository = shortenedRepository;

    public async Task<ShortenedUrl> Handle(
        CreateShortenedUrlCommand command,
        CancellationToken cancellationToken)
    {
        var shortenedUrl = await _shortenerRepository.GetAsync(
            url => url.OriginalUrl == command.Url,
            cancellationToken);

        if (shortenedUrl is not null)
        {
            return shortenedUrl;
        }

        var randomUrl = _urlGenerator.GenerateUniqueUrl();

        var url = ShortenedUrl.Create(
            command.Url,
            $"http://localhost:5069/{randomUrl}");

        await _shortenerRepository.AddAsync(url, cancellationToken);

        return url;
    }
}