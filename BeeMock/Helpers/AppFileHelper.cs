namespace BeeMock;

public static class AppFileHelper
{
    static string fileDir = FileSystem.Current.CacheDirectory;

    public static void DeleteFile(string fileName)
    {
        var filePath = Path.Combine(fileDir, fileName);
        File.Delete(filePath);
    }


    public static string ReadAllText(string fileName, ref DateTime lastWrite)
    {
        var filePath = Path.Combine(fileDir, fileName);
        if (File.Exists(filePath) == false)
            return null;
        lastWrite = new FileInfo(filePath).LastWriteTime;
        return File.ReadAllText(filePath);
    }

    public static void WriteAllText(string fileName, string content)
    {
        string filePath = Path.Combine(fileDir, fileName);
        if (!Directory.Exists(fileDir))
            Directory.CreateDirectory(fileDir);

        File.WriteAllText(filePath, content);

    }

    public static DateTime GetFileLastWriteTime(string fileName)
    {
        var filePath = Path.Combine(fileDir, fileName);
        return new FileInfo(filePath).LastWriteTime;
    }

    public static bool HasFileCacheExpired(string fileName, double minutes)
    {
        var time = GetFileLastWriteTime(fileName);
        return (DateTime.Now - time).TotalMinutes > minutes;
    }
    
}

