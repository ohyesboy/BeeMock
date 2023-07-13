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

public class SrtContent
{
    public string Text { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Segment { get; set; }

    public static List<SrtContent> ParseSRT(string[] liens)
    {
        var content = new List<SrtContent>();
        var segment = 1;
        for (int item = 0; item < liens.Length; item++)
        {
            if (segment.ToString() == liens[item])
            {
                var strStartTime = liens[item + 1].Substring(0, liens[item + 1].LastIndexOf("-->")).Trim().Replace(",",".");
                var strEndTime = liens[item + 1].Substring(liens[item + 1].LastIndexOf("-->") + 3).Trim().Replace(",", ".");

                content.Add(new SrtContent
                {
                    Segment = segment.ToString(),

                    StartTime = TimeSpan.Parse( strStartTime),
                    EndTime = TimeSpan.Parse(strEndTime),
                    Text = liens[item + 2]

                });
                // The block numbers of SRT like 1, 2, 3, ... and so on
                segment++;
                // Iterate one block at a time
                item += 3;
            }
        }

        return content;
    }
}

