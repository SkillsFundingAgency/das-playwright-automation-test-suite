﻿namespace SFA.DAS.MongoDb.DataGenerator.Helpers;

public class MongoDbHelper
{
    private readonly MongoDbConnectionHelper _connectionHelper;
    private readonly IMongoDbDataGenerator _dataGenerator;
    private readonly string _collectionName;

    public MongoDbHelper(MongoDbConnectionHelper connectionHelper, IMongoDbDataGenerator datagenerator)
    {
        _connectionHelper = connectionHelper;
        _dataGenerator = datagenerator;
        _collectionName = _dataGenerator.CollectionName();
    }

    public async Task AsyncCreateData() => await _connectionHelper.AsyncCreateData(_collectionName, _dataGenerator.Data());

    public async Task AsyncDeleteData() => await _connectionHelper.AsyncDeleteData(_collectionName, _dataGenerator.FilterDefinition());
}
