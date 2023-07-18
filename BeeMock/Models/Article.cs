using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
namespace BeeMock;

public partial class Article: ObservableObject
{
    public string Title { get; set; }

    [ObservableProperty]
    string imgSource;

    public string ImgSourceCache
    {
        get
        {
            var file = Path.Combine(AppFileHelper.AppFileDir, ImgSource);
            if(File.Exists(file))
                return file;
            return
                null;
        }
    }


    protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        await ImageCacheHelper.OnPropertyChanged(this, e);
        base.OnPropertyChanged(e);
    }

}


