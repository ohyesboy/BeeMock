namespace BeeMock;

public partial class LibraryView : ContentView
{
    public MainPageModel Model { get; set; }
    public LibraryView(MainPageModel model)
	{
		Model = model;
		InitializeComponent();
		this.BindingContext = Model;
	}

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        Model.Value2 = 3;
    }

    void Button_Clicked_1(System.Object sender, System.EventArgs e)
    {
    }
}
