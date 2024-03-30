using TinyUrl.Application.Common.Interfaces.Services;

namespace TinyUrl.Infrastructure.Services;

public class UrlGenerator : IUrlGenerator
{
    public string GenerateUniqueUrl()
    {
        var random = new Random();
        var base10 = (long)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * random.Next(1342, 2342);
        var base58Chars = "ABCDEFGHJKLMNPQRSTUVWXYZ132456789abcdefghijkmnopqrstuvwxyz";
        var result = "";

        while (base10 > 0)
        {
            var reminder = base10 % 58;

            result += base58Chars[(int)reminder];
            base10 /= 10;
        }

        return result;
    }
}
