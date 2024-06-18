using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace MongoDB.DependencyInjection;

public class MongoClientBuilder(
    IServiceCollection services,
    MongoClient instance)
{
    public MongoClientBuilder AddDatabase(
        string databaseName,
        MongoDatabaseSettings? databaseSettings = null)
    {
        services.AddKeyedScoped(
            databaseName,
            (sp, key) => instance.GetDatabase(databaseName, databaseSettings));

        return this;
    }
}
