using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BeeMock;

public partial class ProgressDash : HorizontalStackLayout, INotifyPropertyChanged
{
   
    private int _Value;
    public int Value { get { return _Value; } set { if (_Value == value) return; _Value = value; OnPropertyChanged(); } }
    
    public ProgressDash()
    {
        InitializeComponent();
        var model = ServiceHelper.GetService<MainPageModel>();

        this.BindingContext = model;
    }

}

public class ProgressDot
{
    public bool Done { get; set; }
}
