using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Plugin.Maui.Audio;

namespace BeeMock;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{

    public MainPageModel Model { get; set; }

    public MainPage(MainPageModel model)
    {
        Model = model;
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Home, Selected = true, Text = "Library", View = new LibraryView(Model) });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Doc_Text, Text = "Vocabs", View = new NewContent2() });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Forumbee, Text = "Bees", View = new LibraryView(Model) });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Sliders, Text = "More", View = new LibraryView(Model) });
        Model.CurrentView = Model.Buttons[0].View;

        InitializeComponent();
        this.BindingContext = Model;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await UpdateData();
    }


    private double PicCacheExpireMinutes = 1000;
    private double DataCacheExpireMinutes = 5;

    public async Task UpdateData()
    {
        //clear cache if expired
        var lastUpdatePics = Preferences.Get("lastUpdatePics", DateTime.MinValue);
        if ((DateTime.Now - lastUpdatePics).TotalMinutes > PicCacheExpireMinutes)
        {
            Preferences.Set("lastUpdatePics", DateTime.Now);
            AppFileHelper.ClearCachedPic();
        }


        //load cache
        Model.Articles = AppCachedObjectHelper.GetCachedObject<ObservableCollection<Article>>("pics/data.json");
        Model.Articles2 = AppCachedObjectHelper.GetCachedObject<ObservableCollection<Article>>("pics/data2.json");

        //update
        var items = await AppCachedObjectHelper.GetFreshObjectWhenExpired<ObservableCollection<Article>>("pics/data.json", DataCacheExpireMinutes);
        Model.Articles = items ?? Model.Articles;

        var items2 = await AppCachedObjectHelper.GetFreshObjectWhenExpired<ObservableCollection<Article>>("pics/data2.json", DataCacheExpireMinutes);
        Model.Articles2 = items2 ?? Model.Articles2;
    }


}

