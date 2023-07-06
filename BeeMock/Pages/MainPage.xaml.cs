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

    private string _Title;
    public string Title { get { return _Title; } set { if (_Title == value) return; _Title = value; OnPropertyChanged(); } }
    public ObservableCollection<Article> Articles { get; set; }
    public ObservableCollection<Article> Articles2 { get; set; }
    public MainPage()
    {
        InitializeComponent();
        Title = "Titles";
        Articles = new ObservableCollection<Article>()
        {
            new Article(){Title="Summer Frenzy", ImgSource="sea.png"},
            new Article(){Title="Tokyo cold", ImgSource="cold.png"},
        };

        Articles2 = new ObservableCollection<Article>()
        {
            new Article(){Title="Explorer the US (Advanced)", ImgSource="small1.png"},
            new Article(){Title="Explore Mexico (Beginner)", ImgSource="small2.png"},
        };
        this.BindingContext = this;

    }
}
public class Article
{
    public string Title { get; set; }
    public string ImgSource { get; set; }

}

