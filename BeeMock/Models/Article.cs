namespace BeeMock;

public class Article
{
    public string Title { get; set; }
    public string ImgSource { get; set; }
    public string ImgSourceUri
    {
        get
        {
            return "https://zhan.blob.core.windows.net/pics/" + ImgSource;
        }
    }
}

