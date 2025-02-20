namespace SFA.DAS.MongoDb.DataGenerator.Helpers;

public interface IMongoDbDataGenerator
{
    string CollectionName();

    BsonDocument[] Data();

    FilterDefinition<BsonDocument> FilterDefinition();
}
