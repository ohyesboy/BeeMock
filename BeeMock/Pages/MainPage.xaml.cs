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
        Model.Title = "test";
        Model.Articles = AppCachedObjectHelper.GetCachedObject<ObservableCollection<Article>>("data.json");

        Model.Articles2.Add(new Article() { Title = "Explorer the US (Advanced)", ImgSource = "small1.png" });
        Model.Articles2.Add(new Article() { Title = "Explorer China (Advanced)", ImgSource = "small1.png" });
        Model.Articles2.Add(new Article() { Title = "Explore Mexico (Beginner)", ImgSource = "small2.png" });
        Model.Articles2.Add(new Article() { Title = "Explore Europe (Beginner)", ImgSource = "small2.png" });

        Model.Value2 = 8;


        InitializeComponent();
        this.BindingContext = Model;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await UpdateData();
    }

    public async Task UpdateData()
    {
        var items =  await AppCachedObjectHelper.GetFreshObjectAndCache<ObservableCollection<Article>>("data.json",0);
        if (items != null)
        {
            Model.Articles = items;
            foreach(var art in items)
            {
                var http = ServiceHelper.GetService<HttpHelper>();
                var filePath = await http.DownloadFileAsync(art.ImgSource);

                //download pics
            }
        }
            

    }


}

