using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
namespace BeeMock;

public partial class Article: ObservableObject
{
    public string Title { get; set; }

    [ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(ImgSourceUri))]
    string imgSource;

    public string ImgSourceUri
    {
        get
        {
            return Path.Combine(FileSystem.Current.CacheDirectory, ImgSource);
        }
    }

    protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
    {

        if(e.PropertyName == "ImgSource")
        {
            var http = ServiceHelper.GetService<HttpHelper>();


            var fileName = ImgSource;
            var filePath = Path.Combine(FileSystem.Current.CacheDirectory, fileName);
            if (AppFileHelper.HasFileCacheExpired(fileName, .1))
            {
                var file = await http.DownloadFileAsync(fileName, true);
                OnPropertyChanged(nameof(ImgSourceUri));
            }
            
        }
        base.OnPropertyChanged(e);
    }

}

