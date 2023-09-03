using UnityEngine;
using UnityEngine.Events;

public class URLUploader : MonoBehaviour
{
	public UnityEvent<string> onVideoUploadSuccess;
	public UnityEvent<string> onVideoUploadFail;

    public AbstractUploader abstractUploader;

    private void Start()
    {
        abstractUploader = new FTPUploader(onVideoUploadSuccess, onVideoUploadFail);
    }

    public void UploadFile(string filePath)
    {
        StartCoroutine(abstractUploader.Upload(filePath));
    }
}
