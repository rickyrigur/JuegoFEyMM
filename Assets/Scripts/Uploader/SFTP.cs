using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SFTP : IConnectionHandler
{
    private string host = null;
    private string user = null;
    private string pass = null;
    private int port = 22;

    private int bufferSize = 2048;

    public SFTP(string hostIP, string userName, string password)
    {
        host = hostIP; user = userName; pass = password;
    }

    public void createDirectory(string newDirectory)
    {
        try
        {
            using (var sftp = new SftpClient(host, port, user, pass))
            {

                sftp.Connect();
                
                if (sftp.IsConnected)
                {
                    sftp.CreateDirectory(newDirectory);
                }

                sftp.Disconnect();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void delete(string deleteFile)
    {
        throw new NotImplementedException();
    }

    public string[] directoryListDetailed(string directory)
    {
        throw new NotImplementedException();
    }

    public string[] directoryListSimple(string directory)
    {
        List<string> directoryList = new List<string>();

        try
        {
            using (var sftp = new SftpClient(host, port, user, pass))
            {

                sftp.Connect();

                var files = sftp.ListDirectory(directory);

                // Filtrar solo los directorios
                foreach (var file in files)
                {
                    // Excluir "." y ".." que son directorios de sistema
                    if (file.IsDirectory && file.Name != "." && file.Name != "..")
                    {
                        directoryList.Add(file.Name);
                    }
                }

                sftp.Disconnect();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

        return directoryList.ToArray();
    }

    public void download(string remoteFile, string localFile)
    {
        throw new NotImplementedException();
    }

    public string getFileCreatedDateTime(string fileName)
    {
        throw new NotImplementedException();
    }

    public string getFileSize(string fileName)
    {
        throw new NotImplementedException();
    }

    public bool peek()
    {
        using (var sftp = new SftpClient(host, port, user, pass))
        {
            try
            {
                bool connection = false;
                sftp.Connect();
                if (sftp.IsConnected)
                {
                    connection = true;
                    Debug.Log("Conexión SFTP exitosa.");
                }
                sftp.Disconnect();
                return connection;

            }
            catch (Exception e)
            {
                Debug.LogError("Error al conectar: " + e.Message);
                return false;
            }
        }
    }

    public void rename(string currentFileNameAndPath, string newFileName)
    {
        throw new NotImplementedException();
    }

    public void upload(string remoteFile, string localFile, Action<string> onSuccess, Action<string> onFail)
    {
        try
        {
            using (var sftp = new SftpClient(host, port, user, pass))
            {
                sftp.Connect();

                if (sftp.IsConnected)
                {

                    using (var fileStream = new FileStream(localFile, FileMode.Open))
                    {
                        sftp.UploadFile(fileStream, remoteFile);
                        onSuccess?.Invoke("Successfully upload");
                    }
                }
                else
                {
                    onFail?.Invoke("Couldn't connect to SFTP server");
                }
                sftp.Disconnect();
            }
        }
        catch (Exception e)
        {
            onFail?.Invoke(e.ToString());
        }
    }
}
