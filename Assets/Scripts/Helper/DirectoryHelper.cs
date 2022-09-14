using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class DirectoryHelper
{
    public static FileInfo SafeCreateDirectory(string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        fileInfo.Directory.Create();
        return fileInfo;
    }
}
