﻿namespace BeeMock;

public static class AppFileHelper
{
    public static string AppFileDir = FileSystem.Current.CacheDirectory;
    public static void ClearCaches()
    {
        Directory.Delete(AppFileDir, true);
    }
    public static void DeleteFile(string fileName)
    {
        var filePath = Path.Combine(AppFileDir, fileName);
        File.Delete(filePath);
    }


    public static string ReadAllText(string fileName, ref DateTime lastWrite)
    {
        var filePath = Path.Combine(AppFileDir, fileName);
        if (File.Exists(filePath) == false)
            return null;
        lastWrite = new FileInfo(filePath).LastWriteTime;
        return File.ReadAllText(filePath);
    }

    public static void WriteAllText(string fileName, string content)
    {
        string filePath = Path.Combine(AppFileDir, fileName);
        if (!Directory.Exists(AppFileDir))
            Directory.CreateDirectory(AppFileDir);

        File.WriteAllText(filePath, content);

    }

    public static DateTime GetFileLastWriteTime(string fileName)
    {
        var filePath = Path.Combine(AppFileDir, fileName);
        return new FileInfo(filePath).LastWriteTime;
    }

    public static bool HasFileCacheExpired(string fileName, double minutes)
    {
        var time = GetFileLastWriteTime(fileName);
        return (DateTime.Now - time).TotalMinutes > minutes;
    }
    
}

