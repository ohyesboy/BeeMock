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
    ObservableCollection<ScriptSegment> segments = new ObservableCollection<ScriptSegment>();

}
