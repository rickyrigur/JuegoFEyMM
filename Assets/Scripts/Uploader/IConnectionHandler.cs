using System;

public interface IConnectionHandler
{
    public void download(string remoteFile, string localFile);

    /* Upload File */
    public void upload(string remoteFile, string localFile, Action<string> onSuccess, Action<string> onFail);

    /* Delete File */
    public void delete(string deleteFile);

    /* Rename File */
    public void rename(string currentFileNameAndPath, string newFileName);

    /* Create a New Directory on the FTP Server */
    public void createDirectory(string newDirectory);

    /* Get the Date/Time a File was Created */
    public string getFileCreatedDateTime(string fileName);

    /* Get the Size of a File */
    public string getFileSize(string fileName);

    /* List Directory Contents File/Folder Name Only */
    public string[] directoryListSimple(string directory);

    /* List Directory Contents in Detail (Name, Size, Created, etc.) */
    public string[] directoryListDetailed(string directory);
    
    /* Checks the server connection */
    public bool peek();
}
