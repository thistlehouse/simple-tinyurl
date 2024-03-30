using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using TinyUrl.Application.Common.Interfaces.Repositories;
using TinyUrl.Application.Common.Interfaces.Services;
using TinyUrl.Infrastructure.Repositories;
using TinyUrl.Infrastructure.Services;

namespace TinyUrl.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .Persistence()
            .AddUrlGeneratorService();

    }

    public static IServiceCollection Persistence(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

        services.AddShortenedUrlRepository();

        return services;
    }

    public static IServiceCollection AddShortenedUrlRepository(this IServiceCollection services)
    {
        services.AddScoped<IShortenedRepository, ShortenedUrlRepository>();

        return services;
    }

    public static IServiceCollection AddUrlGeneratorService(this IServiceCollection services)
    {
        services.AddTransient<IUrlGenerator, UrlGenerator>();

        return services;
    }
}
