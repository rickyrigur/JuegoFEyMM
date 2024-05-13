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
        Debug.Log("START UPLOAD FOR " + filePath);
        if (!File.Exists(filePath))
        {
            Debug.Log("File does not exist");
            onVideoUploadFail?.Invoke("UPLOAD FAILED: FILE DOES NOT EXIST");
            yield return null;
        }
        else
        {
            Debug.Log("File exist");

            FTP ftpClient = new FTP($"{m_FtpHost}", m_FtpUsername, m_FtpPassword);

            if (!ftpClient.peek(directory))
            {
                Debug.Log("Connection with server failed");
                onVideoUploadFail?.Invoke("UPLOAD FAILED: CONNECTION FAILED");
                yield return null;
            }
            else
            {
                string[] directories = directories = ftpClient.directoryListSimple(directory);

                Debug.Log("Directories retrieved");

                bool createFolder = true;
                string directoryName = PlayerPrefs.GetString(MainMenuController.NAME, "");

                foreach (string currentDir in directories)
                {
                    if (currentDir == "")
                        continue;

                    if (currentDir == directoryName)
                    {
                        createFolder = false;
                        Debug.Log($"DIRECTORY FOUND {currentDir}");
                        break;
                    }
                }

                if (createFolder)
                {
                    Debug.Log($"CREATING DIRECTORY {directory}/{directoryName}");

                    ftpClient.createDirectory($"{directory}/{directoryName}");
                }

                string fileName = Path.GetFileName(filePath);
                Debug.Log($"UPLOADING {directory}/{directoryName}/{fileName}");

                ftpClient.upload($"{directory}/{directoryName}/{fileName}", filePath, OnSuccess, OnFail);
                yield return null;
            }
        }
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
