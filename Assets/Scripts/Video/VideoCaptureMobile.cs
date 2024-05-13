using UnityEngine;
using System.IO;
using UnityEngine.UI;
using NatML.Recorders;
using NatML.Recorders.Clocks;
using System.Collections;
using UnityEngine.Android;

public class VideoCaptureMobile : BaseVideoCapture
{
    [SerializeField]
    private GameObject _processingProgressUI;
    [SerializeField]
    private Image _progressBar;
    [SerializeField]
    private string _gameIdentifier;

    [Header("Display Data")]
    private WebCamDevice[] cam_devices;
    private bool hasCameraPermission = false;
    private WebCamTexture webcamTexture;

    [Header("Video Saver Data")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int frames;

    private MP4Recorder recorder;
    private Coroutine _recordVideoCoroutine;
    private bool _recording;

    public Quaternion baseRotation;

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            CheckForCameraDevices();
        }
    }

    private void Update()
    {
        while (!hasCameraPermission)
        {
            CheckForCameraDevices();
        }
    }

    private void CheckForCameraDevices()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
            }
            else
            {
                cam_devices = WebCamTexture.devices;
                if (cam_devices.Length >= 1)
                {
                    //Set front camera
                    webcamTexture = new WebCamTexture(cam_devices[1].name, width, height, frames);
                }
                else
                {
                    //Set back camera
                    webcamTexture = new WebCamTexture(cam_devices[0].name, width, height, frames);
                }

                webcamTexture.Play();
                hasCameraPermission = true;
            }
        }
    }

    protected override void StartVideoCapture()
    {
        if (_recording) return;

        _recording = true;

        // Create a recorder
        recorder = new MP4Recorder(width: width, height: height, frameRate: frames, sampleRate: 48000, channelCount: 2);
        
        //Start recording
        _recordVideoCoroutine = StartCoroutine(Recording());
        
        onStartRecording?.Invoke();
    }

    private IEnumerator Recording()
    {
        var clock = new RealtimeClock();

        while (_recording)
        {
            // Commit video frame
            recorder.CommitFrame(webcamTexture.GetPixels32(), clock.timestamp);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    protected override void StopVideoCapture()
    {
        _recording = false;
        StopCoroutine(_recordVideoCoroutine);
        StopRecord();
    }


    public async void StopRecord()
    {
        string playerName = PlayerPrefs.GetString(MainMenuController.NAME, "");

        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Videos")))
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Videos"));

        _path = Path.Combine(Application.persistentDataPath, $"Videos/{playerName}_{_gameIdentifier}.mp4");

        onEndRecording?.Invoke(_path);

        _processingProgressUI.SetActive(true);

        var recordingPath = await recorder.FinishWriting();

        webcamTexture.Stop();

        // Finish writing

        NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(recordingPath, "Mobile Recording", _path,
            (success, path) =>
            {
                Debug.Log("Media save result: " + success + " " + path);
                OnProccessEnd?.Invoke(path);
            }
        );
        recorder = null;
        webcamTexture = null;
    }
}