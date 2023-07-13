using CommunityToolkit.Mvvm.ComponentModel;

namespace BeeMock;

public partial class ScriptSegment : ObservableObject
{
    public string Text { get; set; }

    [ObservableProperty]
    WordSegment[] wordSegs;

    [ObservableProperty]
    bool isCurrent;

    public double TimeStart { get; set; }
    public double TimeEnd { get; set; }

}

public partial class WordSegment : ObservableObject
{

    public double TimeStart { get; set; }
    public double TimeEnd { get; set; }

    public string Word { get; set; }
    [ObservableProperty]
    bool isCurrent;
}
