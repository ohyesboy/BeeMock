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
    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    [RelayCommand]
    async Task UpdateValue()
    {
        IAudioManager audios = ServiceHelper.GetService<IAudioManager>();
        var player = audios.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("sound1.wav"));
        player.Play();

    }
}
