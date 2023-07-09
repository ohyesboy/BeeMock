using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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


    [RelayCommand]
    void UpdateValue()
    {

        string fileDir = Path.Combine(FileSystem.Current.CacheDirectory,"bee") ;
        string filePath = Path.Combine(fileDir, "input.txt") ;
        if (!Directory.Exists(fileDir))
            Directory.CreateDirectory(fileDir);
        if(!File.Exists(filePath))
        {

            File.WriteAllText(filePath, "this is 测试");
        }

        var content = File.ReadAllText(filePath);

    }
}
