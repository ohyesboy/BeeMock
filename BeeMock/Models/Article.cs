using CommunityToolkit.Mvvm.ComponentModel;
namespace BeeMock;

public partial class Article: ObservableObject
{
    public string Title { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ImgSourceUri))]
    string imgSource;

    public string ImgSourceUri
    {
        get
        {
            return Path.Combine(FileSystem.Current.CacheDirectory, ImgSource);
        }
    }



}

