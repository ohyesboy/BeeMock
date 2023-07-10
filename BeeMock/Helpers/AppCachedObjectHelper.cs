using System.Text.Json;


namespace BeeMock;

public static class AppCachedObjectHelper
{
    public async static Task<T> GetObject<T>(string fileName, double cacheExpireMinutes = 10)
    {

        var lastWrite = DateTime.Now;
        var jsonCached = AppFileHelper.ReadAllText(fileName, ref lastWrite);
        T itemsCached = default(T);
        if (jsonCached != null)
            itemsCached = JsonSerializer.Deserialize<T>(jsonCached);

        var cachedTime = (DateTime.Now - lastWrite).TotalMinutes;
        if (jsonCached == null || cachedTime > cacheExpireMinutes)
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

        //return cached if network failed
        return itemsCached;
    }
}

