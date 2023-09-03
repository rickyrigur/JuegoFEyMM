using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class HTTPUploader : AbstractUploader
{
    private string _url = "http://194.195.86.65/upload";

    public HTTPUploader(string url, UnityEvent<string> onSuccess, UnityEvent<string> onFail) : base(onSuccess, onFail) 
    {
        _url = url;
    }

    public override IEnumerator Upload(string filePath)
    {
        Debug.Log("UPLOADING: " + filePath);
        UnityWebRequest file = UnityWebRequest.Get("file://" + filePath);
        yield return file.SendWebRequest();
        WWWForm form = new WWWForm();
        string playerName = PlayerPrefs.GetString(MainMenuController.NAME, "");
        string fileName = Path.GetFileName(filePath);
        form.AddBinaryData("files[]", file.downloadHandler.data, $"{playerName}/{fileName}");
        var upload = UnityWebRequest.Post(_url, form);
        yield return upload.SendWebRequest();

        if (upload.result != UnityWebRequest.Result.Success)
        {
            onVideoUploadFail?.Invoke("UPLOAD FAILED: " + upload.error);
            Debug.Log("Upload Failed: " + upload.error);
        }
        else
        {
            onVideoUploadSuccess?.Invoke("UPLOAD SUCCESS");
            Debug.Log("Upload Success");
        }
        upload.Dispose();
        file.Dispose();
    }
}
