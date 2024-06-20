namespace FluentFTPExample.Models;
public class FtpInfo
{
    public string HostName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string RemoteFilePath { get; set; }=string.Empty;
    public string LocalFilePath { get; set; } = string.Empty;

}
