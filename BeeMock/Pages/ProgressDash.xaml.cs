using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BeeMock;

public partial class ProgressDash : ContentView, INotifyPropertyChanged
{
	public ObservableCollection<object> DoneValues { get; set; } = new ObservableCollection<object>();
    public ObservableCollection<object> UndoneValues { get; set; } = new ObservableCollection<object> { };
    private int _Value;
	public int Value { get { return _Value; } set { if (_Value == value) return; _Value = value; OnPropertyChanged(); } }
	public ProgressDash()
	{
		InitializeComponent();


        this.BindingContext = this;
	}

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        for (var i = 0; i < 10; i++)
        {
            if (i < Value)
            {
                DoneValues.Add(new object());

            }
            else
            {
                UndoneValues.Add(new object());

            }
        }
    }


}
