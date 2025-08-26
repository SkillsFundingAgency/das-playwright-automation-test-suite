﻿namespace SFA.DAS.Apar.UITests.Project.Helpers.UkprnDataHelpers;

public abstract class AparUkprnBaseDataHelpers
{
    protected readonly Dictionary<string, List<KeyValuePair<string, string>>> _data;

    protected const string ukprnkey = "ukprnkey";
    protected const string providernamekey = "providernamekey";
    protected const string emailkey = "emailkey";
    protected const string passwordkey = "passwordkey";
    protected const string newukprnkey = "newukprnkey";


    public AparUkprnBaseDataHelpers() => _data = [];

    protected (string value1, string value2) GetData(string key, string value1Key, string value2Key)
    {
        var values = FindKeyValuePairs(key);

        return (FindValue(values, value1Key), FindValue(values, value2Key));
    }

    protected (string value1, string value2, string value3) GetData(string key, string value1Key, string value2Key, string value3Key)
    {
        var values = FindKeyValuePairs(key);

        return (FindValue(values, value1Key), FindValue(values, value2Key), FindValue(values, value3Key));
    }

    private List<KeyValuePair<string, string>> FindKeyValuePairs(string key) => _data.TryGetValue(key, out var keyValuePair) ? keyValuePair : throw new KeyNotFoundException($"Can not find data for key {key}");

    private static string FindValue(List<KeyValuePair<string, string>> keyValuePairs, string valuekey) => keyValuePairs.First(x => x.Key == valuekey).Value;
}
