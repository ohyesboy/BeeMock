using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BeeMock;


public partial class VocabPage : ContentPage, INotifyPropertyChanged
{

    public VocabPage()
    {
        InitializeComponent();


        this.BindingContext = this;

    }
}

