using MongoDB.Driver;

namespace MongoDB.DependencyInjection;

public static class MongoClientOptionsExtensions
{
    public static void UseConnectionString(this MongoClientOptions options, string connectionString)
        => options.ConnectionString = connectionString;

    public static void UseUrl(this MongoClientOptions options, MongoUrl url)
        => options.Url = url;

    public static void UseSettings(this MongoClientOptions options, MongoClientSettings settings)
        => options.MongoClientSettings = settings;

    internal static bool HasSettings(this MongoClientOptions options)
        => options.MongoClientSettings is not null;

    internal static bool HasUrl(this MongoClientOptions options)
        => options.Url is not null;

    internal static bool HasConnectionString(this MongoClientOptions options)
        => options.ConnectionString is not null;
}
