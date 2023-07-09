using System.Collections.ObjectModel;
//using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;

public partial class MainPageModel : ObservableObject
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    int value2;

    [ObservableProperty]
    ObservableCollection<IconButtonModel> _Buttons;

    [ObservableProperty]
    ContentView _CurrentView;

    [ObservableProperty]
    ObservableCollection<Article> articles = new ObservableCollection<Article>();
    [ObservableProperty]
    ObservableCollection<Article> articles2 = new ObservableCollection<Article>();

    
    public MainPageModel()
    {
        Buttons = new ObservableCollection<IconButtonModel>();
    }

}

