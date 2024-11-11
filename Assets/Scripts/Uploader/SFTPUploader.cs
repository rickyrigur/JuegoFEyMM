using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class SFTPUploader : AbstractUploader
{
    public SFTPUploader(UnityEvent<string> onSuccess, UnityEvent<string> onFail) : base(onSuccess, onFail) { }

    private string sftpHost = "194.195.86.65";
    private string sftpUsername = "clientejuego";
    private string sftpPassword = "12Violin34__";
    private string directory = "/home/clientejuego/ftp/files";

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

            SFTP sftpClient = new SFTP(sftpHost, sftpUsername, sftpPassword);
            if (!sftpClient.peek())
            {
                Debug.Log("Connection with server failed");
                onVideoUploadFail?.Invoke("UPLOAD FAILED: CONNECTION FAILED");
                yield return null;
            }
            else
            {
                string[] directories = sftpClient.directoryListSimple(directory);

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

                    sftpClient.createDirectory($"{directory}/{directoryName}");
                }

                string fileName = Path.GetFileName(filePath);
                Debug.Log($"UPLOADING {directory}/{directoryName}/{fileName}");

                sftpClient.upload($"{directory}/{directoryName}/{fileName}", filePath, OnSuccess, OnFail);
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
