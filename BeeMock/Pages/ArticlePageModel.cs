using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeeMock;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ArticlePageModel : ObservableObject
{
    [ObservableProperty]
    int id;

    [ObservableProperty]
    string buttonText;



    TimeSpan _CurrentPosition;
    public TimeSpan CurrentPosition { get => _CurrentPosition; set { if (_CurrentPosition == value) return; _CurrentPosition = value; OnPropertyChanged(); } }



    [ObservableProperty]
    ObservableCollection<ParagraphSave> paragraphs = new ObservableCollection<ParagraphSave>();

}

public class ParagraphSave
{
    public string Translation { get; set; }
    public List<SegmentSave> Segments { get; set; }

    public Word[] Words { get; set; }
}

public partial class SegmentSave : ObservableObject
{

    int _Order;
    public int Order { get => _Order; set { if (_Order == value) return; _Order = value; OnPropertyChanged(); } }



    string _Text;
    public string Text { get => _Text; set { if (_Text == value) return; _Text = value; OnPropertyChanged(); } }



    TimeSpan _TimeStart;
    public TimeSpan TimeStart { get => _TimeStart; set { if (_TimeStart == value) return; _TimeStart = value; OnPropertyChanged(); } }


    TimeSpan _TimeEnd;
    public TimeSpan TimeEnd { get => _TimeEnd; set { if (_TimeEnd == value) return; _TimeEnd = value; OnPropertyChanged(); } }



    bool _IsCurrent;
    public bool IsCurrent { get => _IsCurrent; set { if (_IsCurrent == value) return; _IsCurrent = value; OnPropertyChanged(); } }


}

public class Word
{
    public string Text { get; set; }
    public SegmentSave ParentSegment { get; set; }
}