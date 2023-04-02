using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class URLUploader : MonoBehaviour
{
    public string _url = "http://194.195.86.65/Proyecto/videoUploader.php";
    
	public UnityEvent<string> onVideoUploadSuccess;
	public UnityEvent<string> onVideoUploadFail;

    private void Start()
    {
        string path = "C:/Users/JuanP/Desktop/Prueba.txt";
        path = path.Replace("/", @"\");
        UploadFile(path);
    }

    public void UploadFile(string filePath)
    {
        StartCoroutine(Upload(filePath));
    }

    public IEnumerator Upload(string filePath)
    {
        Debug.Log("UPLOADING: " + filePath);
        UnityWebRequest file = UnityWebRequest.Get("file://" + filePath);
        yield return file.SendWebRequest();
        WWWForm form = new WWWForm();
        form.AddBinaryData("dataFile", file.downloadHandler.data, Path.GetFileName(filePath));
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
