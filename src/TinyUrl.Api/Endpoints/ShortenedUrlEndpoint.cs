using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyUrl.Application.Commands.ShortUrl;
using TinyUrl.Application.Queries.RedirectFromShortenedUrl;
using TinyUrl.Contracts.ShortenedUrl;

namespace TinyUrl.Api.Endpoints;

public static class ShortenedUrl
{
    public static void MapShortenerUrlAPi(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", [EndpointName("GetShortenedUrl")] async (
            CreateShortenedUrlRequest request,
            ISender sender) =>
        {
            var command = new CreateShortenedUrlCommand(request.Url);
            var response = await sender.Send(command);

            return Results.CreatedAtRoute(
                routeName: "GetShortenedUrl",
                routeValues: new { ShortenedUrl = response.ShortUrl },
                value: response);
        })
        .WithName("CreateShortenedUrl")
        .WithOpenApi();

        builder.MapGet("/{shortenedUrl}", async (
            string shortenedUrl,
            ISender sender) =>
        {
            var query = new RedirectFromShortenedUrlQuery(shortenedUrl);

            var response = await sender.Send(query);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Redirect(response.OriginalUrl.ToString());
        })
        .WithName("GetShortenedUrl")
        .WithOpenApi();
    }
}