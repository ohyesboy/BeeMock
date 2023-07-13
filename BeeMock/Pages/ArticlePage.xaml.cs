using System.Collections.ObjectModel;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls.Shapes;
using Plugin.Maui.Audio;

namespace BeeMock;


public partial class ArticlePage : ContentPage
{
    private readonly ArticlePageModel model;
    IAudioManager audios = ServiceHelper.GetService<IAudioManager>();

    IAudioPlayer player;

    private System.Timers.Timer aTimer;

    public ArticlePage(ArticlePageModel model)
    {
        InitializeComponent();
        this.BindingContext = model;
        this.model = model;
        model.ButtonText = "Play";
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var stream = await FileSystem.OpenAppPackageFileAsync("pumaatlarge_timeline2.txt");
        StreamReader sr = new StreamReader(stream);
        var content = sr.ReadToEnd().Split(Environment.NewLine);
        stream.Close();

        foreach (var line in content)
        {
            var parts = line.Split('\t', 2);
            if (parts.Length != 2)
                continue;
            var span = TimeSpan.Parse(parts[0]);
            model.Segments.Add(new ScriptSegment { Text = parts[1], TimeStamp = span.TotalSeconds });
        }
        for (int i = 0; i < model.Segments.Count; i++)
        {
            var seg = model.Segments[i];
            var segIndex = model.Segments.IndexOf(seg);


            seg.WordSegs = seg.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new WordSegment { Word = x })
                .ToArray();
            if (seg.WordSegs.Length == 0)
                continue;
            var segTime = seg.TimeStamp / seg.WordSegs.Length;
            for (int wi = 0; wi < seg.WordSegs.Length; wi++)
            {
                double prevSegTime = 0;
                if (i > 0)
                    prevSegTime = model.Segments[i-1].TimeStamp;
                seg.WordSegs[wi].TimeStamp = prevSegTime + segTime * (wi + 1);
            }
          
        }

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Stop();
    }

    private void SetTimer()
    {
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(100);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        //aTimer.AutoReset = true;
        aTimer.Enabled = true;

    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
      
        //var currnet = model.Segments.FirstOrDefault(x => x.TimeStamp > player.CurrentPosition);
        //if (currnet == null)
        //    return;
        //var index = model.Segments.IndexOf(currnet);
        //if (index > 0)
        //    model.Segments[index - 1].IsCurrent = false;
        //currnet.IsCurrent = true;

        foreach (var word in model.Segments.SelectMany(x=>x.WordSegs))
        {
            if (word.TimeStamp < player.CurrentPosition)
                word.IsCurrent = true;
        }
    }
    private void Stop()
    {
        if (player.IsPlaying)
            player.Stop();
        aTimer.Stop();
    }


    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }


    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {


    }

    async void Play_Clicked(System.Object sender, System.EventArgs e)
    {
        if (player == null)
        {
            player = audios.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("pumaatlarge.mp3"));
            SetTimer();
        }
        if (player.IsPlaying)
        {
            player.Pause();
            model.ButtonText = "Play";
        }

        else
        {
            player.Play();
            model.ButtonText = "Pause";
        }


    }

}


[QueryProperty(nameof(Id), nameof(Id))]
public partial class ArticlePageModel : ObservableObject
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    string buttonText;

    [ObservableProperty]
    ObservableCollection<ScriptSegment> segments = new ObservableCollection<ScriptSegment>();

}


public partial class WordSegment : ObservableObject
{

    public double TimeStamp { get; set; }
    public string Word { get; set; }
    [ObservableProperty]
    bool isCurrent;

    partial void OnIsCurrentChanged(bool value)
    {
        if (!value)
            return;
        //
    }
}

public partial class ScriptSegment : ObservableObject
{
    public string Text { get; set; }

    [ObservableProperty]
    WordSegment[] wordSegs;

    [ObservableProperty]
    bool isCurrent;

    public double TimeStamp { get; set; }

}
