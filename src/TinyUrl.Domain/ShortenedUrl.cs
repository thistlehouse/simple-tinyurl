namespace TinyUrl.Domain;

public class ShortenedUrl
{
    public Guid Id { get; private set; }
    public string OriginalUrl { get; private set; }
    public string ShortUrl { get; private set; }
    public int TimesUsed { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    private ShortenedUrl(
        Guid id,
        string originalUrl,
        string shortUrl,
        int timesUsed,
        DateTime createdOnUtc)
    {
        Id = id;
        OriginalUrl = originalUrl;
        ShortUrl = shortUrl;
        TimesUsed = timesUsed;
        CreatedOnUtc = createdOnUtc;
    }

    public static ShortenedUrl Create(
        string originalUrl,
        string shortUrl)
    {
        return new(
            Guid.NewGuid(),
            originalUrl,
            shortUrl,
            0,
            DateTime.UtcNow);
    }

    public void IncreaseTimesUsed()
    {
        TimesUsed++;
    }
}
