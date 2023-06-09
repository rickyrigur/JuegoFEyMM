using UnityEngine;
using UnityEngine.Events;
using FFmpegUnityBind2.Components;
using FFmpegUnityBind2.Demo;

public class VideoCaptureMobile : BaseVideoCapture
{
    [SerializeField]
    private FFmpegREC _recordingCamera;
    [SerializeField]
    private CameraView _cameraView;
    [SerializeField]
    private TextureView _textureView;
    
    protected override void StartVideoCapture()
    {
        _recordingCamera.gameObject.SetActive(true);
        _cameraView.Open();
        _textureView.Open(_cameraView.Texture);
        _recordingCamera.StartREC(RecAudioSource.Mic);
    }

    protected override void StopVideoCapture()
    {
        _recordingCamera.gameObject.SetActive(false);
        _recordingCamera.StopREC();
        _cameraView.Close();
        _textureView.Close();
    }
}