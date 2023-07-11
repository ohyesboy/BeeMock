using System.Collections.ObjectModel;
using System.ComponentModel;
using static Microsoft.Maui.Controls.BindableProperty;

namespace BeeMock;

public partial class ArticleSlideControl : ContentView , INotifyPropertyChanged
{
    public static readonly BindableProperty ArticlesProperty =
        BindableProperty.Create(nameof(Articles), typeof(ObservableCollection<Article>), typeof(ArticleSlideControl));



    public ObservableCollection<Article> Articles { get => GetValue(ArticlesProperty) as ObservableCollection<Article>; set { SetValue(ArticlesProperty, value); OnPropertyChanged(); } }




    public ArticleSlideControl()
    {
        InitializeComponent();

    }

    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ArticlePage),
            new Dictionary<string, object> {
                {"Id",123 }
            });

    }
}
