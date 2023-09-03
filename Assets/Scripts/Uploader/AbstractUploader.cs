using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public abstract class AbstractUploader
{
    public UnityEvent<string> onVideoUploadSuccess;
    public UnityEvent<string> onVideoUploadFail;

    public AbstractUploader(UnityEvent<string> onSuccess, UnityEvent<string> onFail)
    {
        onVideoUploadSuccess = onSuccess;
        onVideoUploadFail = onFail;
    }
    public abstract IEnumerator Upload(string filePath);
}
