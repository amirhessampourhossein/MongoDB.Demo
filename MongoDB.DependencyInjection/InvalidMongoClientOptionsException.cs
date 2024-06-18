namespace MongoDB.DependencyInjection;

public class InvalidMongoClientOptionsException()
    : Exception("invalid 'MongoClient' options!")
{
}
