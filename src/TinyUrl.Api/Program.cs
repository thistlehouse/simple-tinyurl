using TinyUrl.Api.Endpoints;
using TinyUrl.Application;
using TinyUrl.Infrastructure;
using TinyUrl.Infrastructure.Common.Settings;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.Configure<TinyUrlDbSettings>(
        builder.Configuration.GetSection("TinyUrlDb"));

    builder.Services
        .AddApplication()
        .AddInfrastructure();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapShortenerUrlAPi();
    app.UseHttpsRedirection();
    app.Run();
}