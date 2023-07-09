using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;

public partial class ProgressDash : HorizontalStackLayout
{


    double _RecWidth;
    public double RecWidth { get => _RecWidth; set { if (_RecWidth == value) return; _RecWidth = value; OnPropertyChanged(); } }


    double _RecHeight;
    public double RecHeight { get => _RecHeight; set { if (_RecHeight == value) return; _RecHeight = value; OnPropertyChanged(); } }

    public ObservableCollection<ProgressDot> Progress { get; set; } = new ObservableCollection<ProgressDot>();


    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(int), typeof(ProgressDash));

    public int Value { get => (int)GetValue(ValueProperty); set { SetValue(ValueProperty, value); OnPropertyChanged(); } }


    public ProgressDash()
    {
        InitializeComponent();
        var width = DeviceDisplay.Current.MainDisplayInfo.Width;
        var density = DeviceDisplay.Current.MainDisplayInfo.Density;

        RecWidth = width/density/12;
        RecHeight = RecWidth / 2.5;
        
        //this.BindingContext = Model;
    }

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if(propertyName == nameof(Value)) {

            Progress.Clear();
            for (var i = 0; i < 10; i++)
            {

                Progress.Add(new ProgressDot() { Done = i < Value });
            }

        }
    }

}


public class ProgressDot
{
    public bool Done { get; set; }
}
