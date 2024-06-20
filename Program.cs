using FluentFTP;
using FluentFTPExample.Models;
using System.Net;
using System.Text.Json;

Console.WriteLine("Hello, from Ziggy Rafiq!");
string ftpHost = string.Empty;
string ftpUser = string.Empty;
string ftpPass = string.Empty;
string ftpRemoteFilePath = string.Empty;
string ftpLocalFilePath = string.Empty;


 
string jsonFilePath = @"..\..\..\config.json";
string jsonString = File.ReadAllText(jsonFilePath);

AppSettings appSettings = JsonSerializer.Deserialize<AppSettings>(jsonString);

if (appSettings?.FtpInfo != null)
{
    ftpHost = appSettings.FtpInfo.HostName;
    ftpUser = appSettings.FtpInfo.Username;
    ftpPass = appSettings.FtpInfo.Password;
    ftpRemoteFilePath = appSettings.FtpInfo.RemoteFilePath;
    ftpLocalFilePath = appSettings.FtpInfo.LocalFilePath;

}
else
{
    Console.WriteLine("Failed to deserialize FTP settings from config.json.");
    return;
}

using (FtpClient client = new FtpClient(ftpHost))
{
    try
    {
        client.Credentials = new NetworkCredential(ftpUser, ftpPass);

        client.Connect();       

        FtpStatus uploadStatus = client.UploadFile(ftpLocalFilePath, ftpRemoteFilePath);
        if (uploadStatus == FtpStatus.Success)
        {
            Console.WriteLine("File uploaded successfully.");
        }
        else
        {
            Console.WriteLine("File upload failed.");
        }
                
        FtpStatus downloadStatus = client.DownloadFile(ftpLocalFilePath, ftpRemoteFilePath);
        if (downloadStatus == FtpStatus.Success)
        {
            Console.WriteLine("File downloaded successfully.");
        }
        else
        {
            Console.WriteLine("File download failed.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
    finally
    {
        client.Disconnect();
    }
}

Console.ReadLine();