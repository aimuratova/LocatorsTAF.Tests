namespace LocatorsTAF.CoreLayer.Helpers;

public static class FileDownloadHelper
{
    public static string WaitForFile(string folder, string fileName, TimeSpan timeout)
    {
        var path = Path.Combine(folder, fileName);
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {            
            if (File.Exists(path) && !File.Exists(path + ".crdownload"))
                return path;

            Thread.Sleep(250);
        }

        var present = Directory.Exists(folder)
            ? string.Join(", ", Directory.GetFiles(folder).Select(Path.GetFileName))
            : "(folder does not exist)";

        throw new TimeoutException(
            $"'{fileName}' was not downloaded to '{folder}' within {timeout.TotalSeconds}s. Folder contains: {present}");
    }
}
