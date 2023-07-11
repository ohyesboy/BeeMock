using CommunityToolkit.Mvvm.ComponentModel;

namespace BeeMock;


public partial class ArticlePage : ContentPage
{

	public ArticlePage(ArticlePageModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
	}


    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("..", true);
    }
}

[QueryProperty(nameof(Id),nameof(Id))]
public partial class ArticlePageModel: ObservableObject
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    string test;
}
