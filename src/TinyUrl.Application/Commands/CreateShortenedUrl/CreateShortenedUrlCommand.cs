using MediatR;
using TinyUrl.Domain;

namespace TinyUrl.Application.Commands.ShortUrl;

public record CreateShortenedUrlCommand(string Url) : IRequest<ShortenedUrl>;