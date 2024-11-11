using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Linq;

public class FTP : IConnectionHandler
{
    private string host = null;
    private string user = null;
    private string pass = null;
    private FtpWebRequest ftpRequest = null;
    private FtpWebResponse ftpResponse = null;
    private Stream ftpStream = null;
    private int bufferSize = 2048;

    private FtpStatusCode[] okCodes = {
        FtpStatusCode.CommandOK,
        FtpStatusCode.CommandExtraneous,
        FtpStatusCode.DirectoryStatus,
        FtpStatusCode.FileStatus,
        FtpStatusCode.SystemType,
        FtpStatusCode.SendUserCommand,
        FtpStatusCode.ClosingControl,
        FtpStatusCode.ClosingData,
        FtpStatusCode.EnteringPassive,
        FtpStatusCode.LoggedInProceed,
        FtpStatusCode.ServerWantsSecureSession,
        FtpStatusCode.FileActionOK,
        FtpStatusCode.PathnameCreated
    };

    /* Construct Object */
    public FTP(string hostIP, string userName, string password) { host = hostIP; user = userName; pass = password; ServicePointManager.ServerCertificateValidationCallback = ServerCertificateValidationCallback;}

    public void download(string remoteFile, string localFile)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Get the FTP Server's Response Stream */
            ftpStream = ftpResponse.GetResponseStream();
            /* Open a File Stream to Write the Downloaded File */
            FileStream localFileStream = new FileStream(localFile, FileMode.Create);
            /* Buffer for the Downloaded Data */
            byte[] byteBuffer = new byte[bufferSize];
            int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
            /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
            try
            {
                while (bytesRead > 0)
                {
                    localFileStream.Write(byteBuffer, 0, bytesRead);
                    bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
                }
            }
            catch (Exception ex) { Debug.Log(ex.ToString()); }
            /* Resource Cleanup */
            localFileStream.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        return;
    }

    public void upload(string remoteFile, string localFile, Action<string> onSuccess, Action<string> onFail)
    {
        try
        {
            var file = new FileInfo(localFile);
            ftpRequest = WebRequest.Create("ftp://" + host + "/" + remoteFile) as FtpWebRequest;
            ftpRequest.EnableSsl = true;
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = false;
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            FileStream localFileStream = new FileStream(localFile, FileMode.Open);

            byte[] byteBuffer = new byte[bufferSize];

            ftpRequest.ContentLength = file.Length;

            var bufferLength = 2048;
            var buffer = new byte[bufferLength];
            var contentLength = 0;

            int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);

            ftpStream = ftpRequest.GetRequestStream();
            try
            {
                while (bytesSent != 0)
                {
                    ftpStream.Write(byteBuffer, 0, bytesSent);
                    bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                }
            }
            catch (Exception ex) { 
                Debug.Log(ex.ToString()); 
                onFail?.Invoke(ex.ToString());
                localFileStream.Close();
                ftpStream.Close();
                ftpRequest = null;
            }
            
            localFileStream.Close();
            ftpStream.Close();
            ftpRequest = null;
            onSuccess?.Invoke("Successfully upload");
        }
        catch (Exception ex) { 
            Debug.Log(ex.ToString()); 
            onFail?.Invoke(ex.ToString());
            ftpRequest = null;
        }
        return;
    }

    public void delete(string deleteFile)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        return;
    }

    public void rename(string currentFileNameAndPath, string newFileName)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.Rename;
            /* Rename the File */
            ftpRequest.RenameTo = newFileName;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        return;
    }

    public void createDirectory(string newDirectory)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://" + host + "/" + newDirectory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            ftpRequest.EnableSsl = true;
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Resource Cleanup */
            ftpResponse.Close();
            ftpRequest = null;
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        return;
    }

    public string getFileCreatedDateTime(string fileName)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string fileInfo = null;
            /* Read the Full Response Stream */
            try { fileInfo = ftpReader.ReadToEnd(); }
            catch (Exception ex) { Debug.Log(ex.ToString()); }
            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return File Created Date Time */
            return fileInfo;
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return "";
    }

    public string getFileSize(string fileName)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string fileInfo = null;
            /* Read the Full Response Stream */
            try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
            catch (Exception ex) { Debug.Log(ex.ToString()); }
            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return File Size */
            return fileInfo;
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return "";
    }

    public string[] directoryListSimple(string directory)
    {
        try
        {
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://" + host + "/" + directory);
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            ftpRequest.EnableSsl = false;
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpStream = ftpResponse.GetResponseStream();
            StreamReader ftpReader = new StreamReader(ftpStream);
            string directoryRaw = null;
            try { while (ftpReader.Peek() != -1) { 
                    directoryRaw += ftpReader.ReadLine() + "|"; 
                } }
            catch (Exception ex) { Debug.Log(ex.ToString()); }

            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
            catch (Exception ex) { Debug.Log(ex.ToString()); }
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return new string[] { "" };
    }

    public string[] directoryListDetailed(string directory)
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
            /* Log in to the FTP Server with the User Name and Password Provided */
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            /* When in doubt, use these options */
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            /* Specify the Type of FTP Request */
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            /* Establish Return Communication with the FTP Server */
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            /* Establish Return Communication with the FTP Server */
            ftpStream = ftpResponse.GetResponseStream();
            /* Get the FTP Server's Response Stream */
            StreamReader ftpReader = new StreamReader(ftpStream);
            /* Store the Raw Response */
            string directoryRaw = null;
            /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
            try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
            catch (Exception ex) { Debug.Log(ex.ToString()); }
            /* Resource Cleanup */
            ftpReader.Close();
            ftpStream.Close();
            ftpResponse.Close();
            ftpRequest = null;
            /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
            try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
            catch (Exception ex) { Debug.Log(ex.ToString()); }
        }
        catch (Exception ex) { Debug.Log(ex.ToString()); }
        /* Return an Empty string Array if an Exception Occurs */
        return new string[] { "" };
    }

    public bool peek()
    {
        try
        {
            /* Create an FTP Request */
            ftpRequest = (FtpWebRequest)FtpWebRequest.Create("ftp://" + host);
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            ftpRequest.EnableSsl = true;
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = false;
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;

            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpResponse.Close();
            ftpRequest = null;
            return okCodes.Contains(ftpResponse.StatusCode) ;
        }
        catch (Exception ex) {
            Debug.Log(ex.ToString()); 
            return false; }
    }

    private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true; // Accept all certificates
    }
}


