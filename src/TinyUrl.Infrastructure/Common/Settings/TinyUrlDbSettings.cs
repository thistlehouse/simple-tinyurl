using Microsoft.Extensions.Configuration;

namespace TinyUrl.Infrastructure.Common.Settings;

public class TinyUrlDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string Collection { get; set; } = string.Empty;
}