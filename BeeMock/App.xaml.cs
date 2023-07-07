namespace BeeMock;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        UserAppTheme = AppTheme.Light;
        MainPage = new TabbedPage1();
	}

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window =  base.CreateWindow(activationState);
        if(DeviceInfo.Current.Idiom ==  DeviceIdiom.Desktop && DeviceInfo.Current.Platform == DevicePlatform.WinUI)
        {
            window.Width = 450;
            window.Height = 900;
        }
  
        return window;
    }
}
