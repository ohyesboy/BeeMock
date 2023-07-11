using System.Text.Json;


namespace BeeMock;

public static class AppCachedObjectHelper
{

    public static T GetCachedObject<T>(string fileName)
    {
        var lastWrite = DateTime.Now;
        var jsonCached = AppFileHelper.ReadAllText(fileName, ref lastWrite);
        T itemsCached = default(T);
        if (jsonCached != null)
            itemsCached = JsonSerializer.Deserialize<T>(jsonCached);

        return itemsCached;
    }


    public async static Task<T> GetFreshObjectWhenExpired<T>(string fileName, double cacheExpireMinutes)
    {
        var lastWrite = AppFileHelper.GetFileLastWriteTime(fileName);
        var cachedTime = (DateTime.Now - lastWrite).TotalMinutes;
        if (cachedTime > cacheExpireMinutes)
        {
            var http = ServiceHelper.GetService<HttpHelper>();
            var json = await http.GetContentAsync(fileName);

            if (json != null)
            {
                var items = JsonSerializer.Deserialize<T>(json);

                AppFileHelper.WriteAllText(fileName, json);
                return items;

            }
        }
        return default(T);
    }



}

