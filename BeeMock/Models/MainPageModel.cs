using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;

public class MainPageModel : INotifyPropertyChanged
{
    public ObservableCollection<ProgressDot> Progress { get; set; } = new ObservableCollection<ProgressDot>();


    string _Title;
    public string Title { get => _Title; set { if (_Title == value) return; _Title = value; OnPropertyChanged(); } }


    int _Value2;
    public int Value2 { get => _Value2; set { if (_Value2 == value) return; _Value2 = value; OnPropertyChanged(); } }


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

