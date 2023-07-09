using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;

public partial class IconButton : ContentView
{
    ObservableCollection<IconButtonModel> Buttons;

    MainPageModel mainModel;
    public IconButton()
	{
		InitializeComponent();
		mainModel = ServiceHelper.GetService<MainPageModel>();
        //this.BindingContext = mainModel;
		this.Buttons = mainModel.Buttons;
#if __IOS__
		this.stack.Margin = new Thickness(0, 10, 0, 0);
#endif
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
		
		var model = ((sender as BindableObject).BindingContext) as IconButtonModel;

		if (model.Selected)
			return;

        HapticFeedback.Default.Perform(HapticFeedbackType.Click);

        foreach (var b in mainModel.Buttons)
		{
			b.Selected = false;
		}
		model.Selected = true;
		mainModel.CurrentView = model.View;

    }

}

public class IconButtonModel: INotifyPropertyChanged {
    private string _Icon;
    public string Icon { get { return _Icon; } set { if (_Icon == value) return; _Icon = value; OnPropertyChanged(); } }
    private string _Text;
    public string Text { get { return _Text; } set { if (_Text == value) return; _Text = value; OnPropertyChanged(); } }
	bool _Selected;
	public bool Selected { get => _Selected; set { if (_Selected == value) return; _Selected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected))); } }

	ContentView _View;
	public ContentView View { get => _View; set { if (_View == value) return; _View = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(View))); } }

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

