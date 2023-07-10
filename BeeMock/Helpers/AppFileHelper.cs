namespace BeeMock;

public static class AppFileHelper
{
    static string fileDir = Path.Combine(FileSystem.Current.CacheDirectory);
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
}

