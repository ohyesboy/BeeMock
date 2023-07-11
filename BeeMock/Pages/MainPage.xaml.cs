using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
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


    private double CacheExpireMinutes = 1;
    public async Task UpdateData()
    {
        //clear cache if expired
        var lastUpdate = Preferences.Get("lastUpdate", DateTime.MinValue);
        if ((DateTime.Now - lastUpdate).TotalMinutes > CacheExpireMinutes)
        {
            Preferences.Set("lastUpdate", DateTime.Now);
            AppFileHelper.ClearCaches();
        }


        //load cache
        Model.Articles = AppCachedObjectHelper.GetCachedObject<ObservableCollection<Article>>("data.json");
        Model.Articles2.Add(new Article() { Title = "Explorer the US (Advanced)", ImgSource = "small1.png" });

        //update
        var items = await AppCachedObjectHelper.GetFreshObjectAndCache<ObservableCollection<Article>>("data.json", 0);
        Model.Articles = items ?? Model.Articles;

    }


}

