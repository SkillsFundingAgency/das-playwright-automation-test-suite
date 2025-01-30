using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers;

public abstract class SqlDbHelper(ObjectContext objectContext, string connectionString) : SqlDbBaseHelper(objectContext, connectionString)
{
    protected static string Func(List<string> x) => x.IsNoDataFound() ? string.Empty : x.FirstOrDefault();

    #region ExecuteSqlCommand

    protected async Task<int> ReTryExecuteSqlCommand(string queryToExecute) => await SqlDbRetryHelper.RetryOnException(async () => await ExecuteSqlCommand(queryToExecute));

    #endregion

    protected async Task<List<string>> GetData(string query) => await GetData(query, connectionString);

    protected async Task<List<string>> GetData(string query, string connectionstring) => await GetData(query, connectionstring, null);

    protected async Task<List<string>> GetData(string query, Dictionary<string, string> parameters) => await GetData(query, connectionString, parameters);

    protected async Task<List<string>> GetData(string query, string connectionstring, Dictionary<string, string> parameters)
    {
        (List<object[]> data, int noOfColumns) data = await GetListOfData(query, connectionstring, parameters);

        var returnItems = new List<string>();

        for (int i = 0; i < data.noOfColumns; i++)
        {
            if (data.data.Count == 0) returnItems.Add(string.Empty);
            else returnItems.Add(data.data[0][i].ToString());
        }

        return returnItems;
    }

    protected async Task<List<string[]>> GetMultipleData(string query)
    {
        var result = await GetListOfMultipleData([query]);

        return result.FirstOrDefault();
    }

    protected async Task<List<List<string[]>>> GetListOfMultipleData(List<string> query)
    {
        List<(List<object[]> data, int noOfColumns)> multidatas = await SqlDbRetryHelper.RetryOnException(async () => await GetMultipleListOfData(query));

        var multireturnItems = new List<List<string[]>>();

        foreach (var (data, noOfColumns) in multidatas)
        {
            var returnItems = new List<string[]>();

            var datalist = data;

            var noOfRows = datalist.Count;

            if (noOfRows == 0) returnItems.Add(new string[noOfColumns]);

            for (int i = 0; i < noOfRows; i++)
            {
                var items = new string[datalist[i].Length];

                for (int j = 0; j < items.Length; j++)
                {
                    var item = datalist[i][j].ToString();

                    items[j] = item;
                }
                returnItems.Add(items);
            }
            multireturnItems.Add(returnItems);
        }
        return multireturnItems;
    }

    protected async Task<string> GetNullableData(string queryToExecute)
    {
        var data = await GetData(queryToExecute);

        if (data.Count == 0) return string.Empty;

        else return data[0];
    }

    protected async Task<string> GetDataAsString(string queryToExecute) => Convert.ToString(await GetDataAsObject(queryToExecute));

    protected async Task<object> GetDataAsObject(string queryToExecute)
    {
        var result = await GetListOfData(queryToExecute);

        return result[0][0];
    }
    protected async Task<object> GetDataAsObject(string queryToExecute, Dictionary<string, string> parameters)
    {
        var (data, _) = await GetListOfData(queryToExecute, connectionString, parameters);

        return data[0][0];
    }


    protected async Task<object> WaitAndGetDataAsObject(string queryToExecute)
    {
        waitForResults = true;

        return await GetDataAsObject(queryToExecute);
    }
}
