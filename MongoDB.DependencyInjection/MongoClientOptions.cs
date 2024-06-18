using MongoDB.Driver;

namespace MongoDB.DependencyInjection;

public class MongoClientOptions
{
    public string? ConnectionString { get; set; }

    public MongoUrl? Url { get; set; }

    public MongoClientSettings? MongoClientSettings { get; set; }
}
