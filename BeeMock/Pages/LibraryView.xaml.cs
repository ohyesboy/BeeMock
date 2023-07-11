using System.Diagnostics;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;

namespace BeeMock;

public partial class LibraryView : ContentView
{
    public MainPageModel Model { get; set; }
    public LibraryView(MainPageModel model)
	{
		Model = model;
		InitializeComponent();
		this.BindingContext = Model;

      
    }
    IAudioManager audios = ServiceHelper.GetService<IAudioManager>();

    IAudioPlayer player;

    [RelayCommand]
    async Task UpdateValue()
    {
        if(player == null)
        {
            player = audios.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("pumaatlarge.mp3"));
        }
        player.Play();
        player.Seek(10);

    }

    [RelayCommand]
    async Task Pause()
    {
        Debug.WriteLine($"--> IsPlaying = {player.IsPlaying}");
        if(player.IsPlaying)
        {
            player.Pause();
        }
        else {
            player.Play();
        }

    }
}
