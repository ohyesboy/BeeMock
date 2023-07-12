using System.Collections.ObjectModel;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
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

        foreach(var line in content)
        {
            var parts = line.Split('\t', 2);
            if (parts.Length != 2)
                continue;
            var span = TimeSpan.Parse(parts[0]);
            model.Segments.Add(new ScriptSegment { Text = parts[1], TimeStamp = span.TotalSeconds });
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
        aTimer = new System.Timers.Timer(0.2);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimedEvent;
        //aTimer.AutoReset = true;
        aTimer.Enabled = true;

    }

    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        var currnet = model.Segments.FirstOrDefault(x => x.TimeStamp > player.CurrentPosition);
        if (currnet == null)
            return;
        var index = model.Segments.IndexOf(currnet);
        if (index > 0)
            model.Segments[index - 1].IsCurrent = false;
        currnet.IsCurrent = true;
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


[QueryProperty(nameof(Id),nameof(Id))]
public partial class ArticlePageModel: ObservableObject
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    string buttonText;

    [ObservableProperty]
    ObservableCollection<ScriptSegment> segments = new ObservableCollection<ScriptSegment>();

}


public partial class ScriptSegment : ObservableObject
{
    [ObservableProperty]
    string text;

    [ObservableProperty]
    bool isCurrent;

    [ObservableProperty]
    double timeStamp;

}
