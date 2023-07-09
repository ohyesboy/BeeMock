using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BeeMock;

public partial class ProgressDash : HorizontalStackLayout, INotifyPropertyChanged
{


    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(int), typeof(ProgressDash));

    public int Value { get => (int)GetValue(ValueProperty); set { SetValue(ValueProperty, value); OnPropertyChanged(); } }


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
