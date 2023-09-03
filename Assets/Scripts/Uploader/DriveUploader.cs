using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityGoogleDrive;

public class DriveUploader : AbstractUploader
{
    public DriveUploader(UnityEvent<string> onSuccess, UnityEvent<string> onFail) : base(onSuccess, onFail) {}

    public override IEnumerator Upload(string filePath)
    {
        Debug.Log("UPLOADING: " + filePath);
        UnityWebRequest file = UnityWebRequest.Get("file://" + filePath);
        yield return file.SendWebRequest();
        string playerName = "JP";//PlayerPrefs.GetString(MainMenuController.NAME, "");
        string fileName = Path.GetFileName(filePath);

        var driveFile = new UnityGoogleDrive.Data.File() { Name = $"{playerName}_{fileName}", Content = file.downloadHandler.data };
        var request = GoogleDriveFiles.Create(driveFile);
        yield return request.Send();

        if (request.IsError)
        {
            onVideoUploadFail?.Invoke("UPLOAD FAILED: " + request.Error);
            Debug.Log("Upload Failed: " + request.Error);
        }
        else
        {
            onVideoUploadSuccess?.Invoke("UPLOAD SUCCESS");
            Debug.Log("Upload Success");
        }
        request.Dispose();
        file.Dispose();
    }
}
