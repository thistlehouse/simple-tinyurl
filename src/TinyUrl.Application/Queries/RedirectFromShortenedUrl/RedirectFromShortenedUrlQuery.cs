using MediatR;
using TinyUrl.Domain;

namespace TinyUrl.Application.Queries.RedirectFromShortenedUrl;

public record RedirectFromShortenedUrlQuery(string ShortenedUrl) : IRequest<ShortenedUrl>;