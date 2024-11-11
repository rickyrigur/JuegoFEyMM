using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class FTPUploader : AbstractUploader
{
    public FTPUploader(UnityEvent<string> onSuccess, UnityEvent<string> onFail) : base(onSuccess, onFail) {}

    private string m_FtpHost = "194.195.86.65";
    private string m_FtpUsername = "clientejuego";
    private string m_FtpPassword = "12Violin34__";
    private string directory = "home/clientejuego/ftp";

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
            if (!ftpClient.peek())
            {
                Debug.Log("Connection with server failed");
                onVideoUploadFail?.Invoke("UPLOAD FAILED: CONNECTION FAILED");
                yield return null;
            }
            else
            {
                string[] directories = ftpClient.directoryListSimple(directory);

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
