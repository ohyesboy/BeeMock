using System.Diagnostics;

namespace BeeMock;

public class HttpHelper {
    private readonly Uri baseUri;
    private HttpClient _client;
    public HttpHelper(string baseUri)
    {
        this._client = new HttpClient();
        this.baseUri = new Uri(baseUri);
    }

    public async Task<string> GetContentAsync(string partialUrl)
    {
        Uri uri = new Uri(baseUri, partialUrl);
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return str;
                
            }
            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);

        }
        return null;
    }
}

