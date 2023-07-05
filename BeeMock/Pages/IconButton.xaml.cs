namespace BeeMock;

public partial class IconButton : ContentView
{

	private Color _TextColor;
	public Color TextColor { get { return _TextColor; } set { if (_TextColor == value) return; _TextColor = value; OnPropertyChanged(); } }
	private string _Icon;
	public string Icon { get { return _Icon; } set { if (_Icon == value) return; _Icon = value; OnPropertyChanged(); } }
	private string _Text;
	public string Text { get { return _Text; } set { if (_Text == value) return; _Text = value; OnPropertyChanged(); } }
    public IconButton()
	{
		InitializeComponent();
		this.BindingContext = this;

	}
}