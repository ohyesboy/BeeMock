using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{

    public MainPageModel Model { get; set; }

    public MainPage(MainPageModel model)
    {
        var screenHeight2 = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
        Model = model;
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Home, Selected = true, Text = "Library", View = new LibraryView(Model) });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Doc_Text, Text = "Vocabs", View = new NewContent2() });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Forumbee, Text = "Bees", View = new LibraryView(Model) });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Sliders, Text = "More", View = new LibraryView(Model) });
        Model.CurrentView = Model.Buttons[0].View;
        Model.Title = "test";

        Model.Articles.Add(new Article() { Title = "Summer Frenzy", ImgSource = "sea.png" });
        Model.Articles.Add(new Article() { Title = "Tokyo cold", ImgSource = "cold.png" });

        Model.Articles2.Add(new Article() { Title = "Explorer the US (Advanced)", ImgSource = "small1.png" });
        Model.Articles2.Add(new Article() { Title = "Explorer China (Advanced)", ImgSource = "small1.png" });
        Model.Articles2.Add(new Article() { Title = "Explore Mexico (Beginner)", ImgSource = "small2.png" });
        Model.Articles2.Add(new Article() { Title = "Explore Europe (Beginner)", ImgSource = "small2.png" });

        Model.Value2 = 8;


        InitializeComponent();
        this.BindingContext = Model;

        var screenHeight = Microsoft.Maui.Devices.DeviceDisplay.MainDisplayInfo.Height;
        //var bottomHeight = grid.RowDefinitions[1].Height.Value;
        //grid.RowDefinitions[0].Height = screenHeight - bottomHeight;
    }

  
}

