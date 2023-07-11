using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
namespace BeeMock;

public class ImageCacheHelper {
    public static async Task OnPropertyChanged(ObservableObject obj, PropertyChangedEventArgs e)
    {

        if (e.PropertyName.EndsWith("ImgSource"))
        {
            var type = obj.GetType();
            var http = ServiceHelper.GetService<HttpHelper>();


            var fileName = type.GetProperty(e.PropertyName).GetValue(obj) as string ;
            var filePath = Path.Combine(AppFileHelper.AppFileDir, fileName);
            if (!File.Exists(filePath))
            {
                Debug.WriteLine("DOWNLOADING -> "+fileName);

                await http.DownloadFileAsync(fileName, true);
                type.InvokeMember("OnPropertyChanged",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Instance,
                    binder: null,
                    obj,
                    new object[] { e.PropertyName + "Cache" });
             
            }

        }
    }
}

