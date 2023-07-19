using UnityEngine;
using FFmpegUnityBind2.Components;
using FFmpegUnityBind2.Demo;
using FFmpegUnityBind2;
using System.IO;
using UnityEngine.UI;

public class VideoCaptureMobile : BaseVideoCapture
{
    [SerializeField]
    private FFmpegREC _recordingCamera;
    [SerializeField]
    private CameraView _cameraView;
    [SerializeField]
    private GameObject _processingProgressUI;
    [SerializeField]
    private Image _progressBar;
    [SerializeField]
    private string _gameIdentifier;
    [SerializeField]
    private Documento _compilationDocumentLogger;

    protected override void StartVideoCapture()
    {
        string playerName = PlayerPrefs.GetString(MainMenuController.NAME, "");
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Videos")))
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Videos"));
        _path = Path.Combine(Application.persistentDataPath, $"Videos/{playerName}_{_gameIdentifier}.mp4");
        IFFmpegCallbacksHandler callbackHandler = new VideoCallbackHandler(OnProccessEnd, _path, _progressBar, _compilationDocumentLogger);
        onStartRecording?.Invoke();

        if (_cameraView != null)
            _cameraView.Open();

        if (_recordingCamera != null)
        {
            _recordingCamera.gameObject.SetActive(true);
            _recordingCamera.StartREC(RecAudioSource.Mic, _path, callbackHandler);
        }
    }

    protected override void StopVideoCapture()
    {
        onEndRecording?.Invoke(_path);
        if (_recordingCamera != null)
        {
            _processingProgressUI.SetActive(true);
            _recordingCamera.StopREC();
        }

        if (_cameraView != null)
            _cameraView.Close();
    }
}