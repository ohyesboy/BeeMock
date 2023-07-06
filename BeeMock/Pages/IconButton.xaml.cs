using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;

public partial class IconButton : ContentView
{

	public ObservableCollection<IconButtonModel> Buttons { get; set; } = new ObservableCollection<IconButtonModel>();
    public IconButton()
	{
		InitializeComponent();
		Buttons.Add(new IconButtonModel { Icon = Icons.Doc_Text, Selected = true, Text = "Doc" });
        Buttons.Add(new IconButtonModel { Icon = Icons.Menu, Text = "Options" });
		var index = 0;
		foreach(var b in Buttons)
		{
			b.Order = index++;
		}
        this.BindingContext = this;

	}

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
		

    }
}

public class IconButtonModel: INotifyPropertyChanged {
    private string _Icon;
    public string Icon { get { return _Icon; } set { if (_Icon == value) return; _Icon = value; OnPropertyChanged(); } }
    private string _Text;
    public string Text { get { return _Text; } set { if (_Text == value) return; _Text = value; OnPropertyChanged(); } }
	bool _Selected;
	public bool Selected { get => _Selected; set { if (_Selected == value) return; _Selected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected))); } }
	int _Order;
	public int Order { get => _Order; set { if (_Order == value) return; _Order = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Order))); } }
	public event PropertyChangedEventHandler PropertyChanged;

	public void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}


}

public class IconButtonTemplateSelector : DataTemplateSelector
{
	public DataTemplate Selected { get; set; }
	public DataTemplate UnSelected { get; set; }
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
		var model = item as IconButtonModel;
		if (model.Selected)
			return Selected;
		else
			return UnSelected;
    }
}

