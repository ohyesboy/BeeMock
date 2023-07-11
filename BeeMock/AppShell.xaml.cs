namespace BeeMock;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ArticlePage), typeof(ArticlePage));
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
	}
}
