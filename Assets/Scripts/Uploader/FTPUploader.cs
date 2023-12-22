using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class FTPUploader : AbstractUploader
{
    public FTPUploader(UnityEvent<string> onSuccess, UnityEvent<string> onFail) : base(onSuccess, onFail) {}

    private string m_FtpHost = "194.195.86.65";
    private string m_FtpUsername = "cliente-ftp";
    private string m_FtpPassword = "juan3-nati2-mati1";
    private string directory = "/archivos";

    public override IEnumerator Upload(string filePath)
    {

        UnityWebRequest file = UnityWebRequest.Get("file://" + filePath);
        yield return file.SendWebRequest();
        if (file == null)
        {
            onVideoUploadFail?.Invoke("UPLOAD FAILED: FILE IS NULL");
        }
        FTP ftpClient = new FTP($"{m_FtpHost}", m_FtpUsername, m_FtpPassword);
        string[] directories = ftpClient.directoryListSimple(directory);
        bool createFolder = true;
        string directoryName = PlayerPrefs.GetString(MainMenuController.NAME, "");

        foreach (string currentDir in directories)
        {
            if (currentDir == "")
                continue;

            if (currentDir == directoryName)
            {
                createFolder = false;
                break;
            }
        }

        if (createFolder)
        {
            ftpClient.createDirectory($"{directory}/{directoryName}");
        }

        string fileName = Path.GetFileName(filePath);
        ftpClient.upload($"{directory}/{directoryName}/{fileName}", filePath, OnSuccess, OnFail);
    }

    private void OnSuccess(string log)
    {
        onVideoUploadSuccess?.Invoke(log);
    }

    private void OnFail(string log)
    {
        onVideoUploadFail?.Invoke(log);
    }
}
