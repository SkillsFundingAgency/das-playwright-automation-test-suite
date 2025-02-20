namespace SFA.DAS.MongoDb.DataGenerator.Helpers;

public abstract class EmpRefFilterDefinition(MongoDbDataHelper helper)
{
    protected readonly MongoDbDataHelper mongoDbDatahelper = helper;

    public FilterDefinition<BsonDocument> FilterDefinition() => Builders<BsonDocument>.Filter.Eq("empref", mongoDbDatahelper.EmpRef);
}
