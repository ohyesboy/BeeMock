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

    [ObservableProperty]
    TimeSpan currentPosition;


    [ObservableProperty]
    ObservableCollection<ParagraphSave> paragraphs = new ObservableCollection<ParagraphSave>();

    [ObservableProperty]
    ObservableCollection<SegmentSave> segments = new ObservableCollection<SegmentSave>();
}

public class ParagraphSave
{
    public string Translation { get; set; }
    public List<SegmentSave> Segments { get; set; }
}

public partial class SegmentSave : ObservableObject
{
    [ObservableProperty]
    int order;

    [ObservableProperty]
    string text;


    TimeSpan _TimeStart;
    public TimeSpan TimeStart { get => _TimeStart; set { if (_TimeStart == value) return; _TimeStart = value; OnPropertyChanged(); } }


    TimeSpan _TimeEnd;
    public TimeSpan TimeEnd { get => _TimeEnd; set { if (_TimeEnd == value) return; _TimeEnd = value; OnPropertyChanged(); } }


    [ObservableProperty]
    bool isCurrent;

}
