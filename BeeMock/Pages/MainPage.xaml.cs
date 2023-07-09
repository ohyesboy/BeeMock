using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;

public static class Icons
{
    public const string Search = "\uE801";
    public const string Menu = "\uF0C9";
    public const string Home_Outline = "\uE800";
    public const string Home = "\uE802";
    public const string Doc_Text = "\uF0F6";
    public const string Forumbee = "\uF211";
    public const string Sliders = "\uF1DE";
}

public partial class MainPage : ContentPage, INotifyPropertyChanged
{

    public MainPageModel Model { get; set; }

    private string _Title;
    public string Title { get { return _Title; } set { if (_Title == value) return; _Title = value; OnPropertyChanged(); } }
  
    public MainPage(MainPageModel model)
    {
        Model = model;
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Home, Selected = true, Text = "Library", View = new LibraryView(Model) });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Doc_Text, Text = "Vocabs", View = new NewContent2() });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Forumbee, Text = "Bees", View = new LibraryView(Model) });
        Model.Buttons.Add(new IconButtonModel { Icon = Icons.Sliders, Text = "More", View = new LibraryView(Model) });
        Model.CurrentView = Model.Buttons[0].View;
        InitializeComponent();
        Title = "Titles";


        Model.Articles.Add(new Article() { Title = "Summer Frenzy", ImgSource = "sea.png" });
        Model.Articles.Add(new Article() { Title = "Tokyo cold", ImgSource = "cold.png" });

        Model.Articles2.Add(new Article() { Title = "Explorer the US (Advanced)", ImgSource = "small1.png" });
       Model.Articles2.Add(new Article() { Title = "Explore Mexico (Beginner)", ImgSource = "small2.png" });

        for (var i = 0; i < 10; i++)
        {

            Model.Progress.Add(new ProgressDot() { Done = i < 4 });
        }


        this.BindingContext = Model;

    }
}

public class MainPageModel: INotifyPropertyChanged
{
    public ObservableCollection<ProgressDot> Progress { get; set; } = new ObservableCollection<ProgressDot>();


    ObservableCollection<IconButtonModel> _Buttons;
    public ObservableCollection<IconButtonModel> Buttons { get => _Buttons; set { if (_Buttons == value) return; _Buttons = value; OnPropertyChanged(); } }


    ContentView _CurrentView;
    public ContentView CurrentView { get => _CurrentView; set { if (_CurrentView == value) return; _CurrentView = value; OnPropertyChanged(); } }


    public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>();
    public ObservableCollection<Article> Articles2 { get; set; } = new ObservableCollection<Article>();


    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public MainPageModel()
    {
        Buttons = new ObservableCollection<IconButtonModel>();
    }

}

public class Article
{
    public string Title { get; set; }
    public string ImgSource { get; set; }

}

