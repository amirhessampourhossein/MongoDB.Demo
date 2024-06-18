using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MongoDB.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static MongoClientBuilder AddMongoClient(
        this IServiceCollection services,
        Action<MongoClientOptions> optionsBuilder)
    {
        MongoClientOptions options = new();
        optionsBuilder(options);

        MongoClient instance;
        if (options.HasSettings())
            instance = new(options.MongoClientSettings);
        else if (options.HasUrl())
            instance = new(options.Url);
        else if (options.HasConnectionString())
            instance = new(options.ConnectionString);
        else
            throw new InvalidMongoClientOptionsException();

        services.AddScoped(sp => instance);

        return new(services, instance);
    }
}
