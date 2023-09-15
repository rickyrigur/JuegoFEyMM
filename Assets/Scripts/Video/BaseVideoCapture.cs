using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseVideoCapture : MonoBehaviour, IVideoCapture
{
    public UnityEvent onStartRecording;
    public UnityEvent<string> onEndRecording;
    public UnityEvent<string> OnProccessEnd;

    protected string _path;

    public void StartRecording()
    {
#if UNITY_ANDROID
        SetPath();
        StartVideoCapture();
#endif
    }

    public void StopRecording()
    {
        StopVideoCapture();
    }

    abstract protected void StartVideoCapture();
    abstract protected void StopVideoCapture();

    protected void SetPath()
    {
        string timeStamp = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("/", "");
        string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
        string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);
        filepath = filepath.Replace("/", @"\");
        _path = filepath;
    }

    private void OnDestroy()
    {
        StopRecording();
    }

}
