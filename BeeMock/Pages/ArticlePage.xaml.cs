using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        await LoadModel();
    }

    private async Task LoadModel()
    {
        var stream = await FileSystem.OpenAppPackageFileAsync("pumaatlarge_all.json");
        StreamReader sr = new StreamReader(stream);
        var jsonContent = sr.ReadToEnd();
        stream.Close();
        var paras = JsonSerializer.Deserialize<List<ParagraphSave>>(jsonContent);
        model.Paragraphs = new ObservableCollection<ParagraphSave>(paras);
        foreach (var p in paras)
        {
            p.Words = p.Segments
                .SelectMany(x => x.Text.Split(' ').Select(x => x + " ")
                    .Select(w => new Word { Text = w, ParentSegment = x }))
                .ToArray();
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
        if (!player.IsPlaying)
            return;
        model.CurrentPosition = TimeSpan.FromSeconds(player.CurrentPosition);

        var currentPosSegCutOff = player.CurrentPosition + 0.3;
        foreach (var seg in model.Paragraphs.SelectMany(x => x.Segments))
        {
            bool segInRange = seg.TimeStart.TotalSeconds < currentPosSegCutOff
                    && seg.TimeEnd.TotalSeconds > currentPosSegCutOff;
            if (segInRange)
            {
                
                if (seg.IsCurrent != true)
                {
                    seg.IsCurrent = true;
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        // Code to run on the main thread
                       
                        segsView.ScrollTo(seg, position: ScrollToPosition.MakeVisible);
                        Debug.WriteLine("-->SCROLL to " + seg.Text);
                    });
                }
            }
            else
                seg.IsCurrent = false;

        }

    }
    private void Stop()
    {
        if (player == null)
            return;
        if (player.IsPlaying)
            player.Stop();
        aTimer.Stop();
    }


    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
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

    [RelayCommand]
    void SegTap(SegmentSave seg)
    {
        if (player == null)
            return;
        player.Seek(seg.TimeStart.TotalSeconds - 0.3);

    }


}



