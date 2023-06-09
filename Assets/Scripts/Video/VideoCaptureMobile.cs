using UnityEngine;
using FFmpegUnityBind2.Components;
using FFmpegUnityBind2.Demo;
using FFmpegUnityBind2;
using System.IO;

public class VideoCaptureMobile : BaseVideoCapture
{
    [SerializeField]
    private FFmpegREC _recordingCamera;
    [SerializeField]
    private CameraView _cameraView;
    
    protected override void StartVideoCapture()
    {
        _path = Path.Combine(Application.temporaryCachePath, "REC.mp4");
        IFFmpegCallbacksHandler callbackHandler = new VideoCallbackHandler(OnProccessEnd, _path);

        if (_cameraView != null)
            _cameraView.Open();

        if (_recordingCamera != null)
        {
            _recordingCamera.gameObject.SetActive(true);
            _recordingCamera.StartREC(RecAudioSource.Mic, callbackHandler);
        }
    }

    protected override void StopVideoCapture()
    {
        if (_recordingCamera != null)
        {
            _recordingCamera.StopREC();
        }

        if (_cameraView != null)
            _cameraView.Close();
    }
}