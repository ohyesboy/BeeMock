using System.Diagnostics;
using System.Text;
using CommunityToolkit.Maui.Storage;

namespace BeeMock;

public class HttpHelper {
    private readonly Uri baseUri;
   
    public HttpHelper(string baseUri)
    {
        
        this.baseUri = new Uri(baseUri);
    }

    public async Task<string> GetContentAsync(string partialUrl)
    {
        var _client = new HttpClient();
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


    public async Task<string> DownloadFileAsync(string fileName, bool overwriteExisting = false)
    {
        var _client = new HttpClient();
        string partialUrl = fileName;
        Uri uri = new Uri(baseUri, partialUrl+"?t="+DateTime.Now.Ticks);
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                //save stream

                var fileDir =AppFileHelper.AppFileDir;
                var filePath = Path.Combine(fileDir, fileName);
  
             
                if (File.Exists(filePath) && !overwriteExisting)
                    return filePath;

                Directory.CreateDirectory(fileDir);
                using (var fileStream = File.Create(filePath))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
                return filePath;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return null;
    }
}

