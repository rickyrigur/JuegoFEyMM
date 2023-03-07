using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class URLUploader : MonoBehaviour
{
    private string _url = "http://localhost:8000/videoUploader.php"; //Modifiquenlo por la ruta donde tengan el php/python para
                                                                  //subir los archivos
    
	public UnityEvent onVideoUploadSuccess;
	public UnityEvent onVideoUploadFail;
	
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
        form.AddBinaryData("dataFile", file.downloadHandler.data, Path.GetFileName(filePath)); //el file name que en este
                                                    //caso es dataFile tiene que llamarse de la misma forma en el php/python

        var upload = UnityWebRequest.Post(_url, form);
        yield return upload.SendWebRequest();

        if (upload.result != UnityWebRequest.Result.Success)
        {
            onVideoUploadFail?.Invoke();
            Debug.Log("Upload Failed: " + upload.error);
        }
        else
        {
            onVideoUploadSuccess?.Invoke();
            Debug.Log("Upload Success");
        }
        upload.Dispose();
        file.Dispose();
    }
}
