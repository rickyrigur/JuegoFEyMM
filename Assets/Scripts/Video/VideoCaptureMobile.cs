using UnityEngine;
using FFmpegUnityBind2.Components;
using FFmpegUnityBind2.Demo;
using FFmpegUnityBind2;
using System.IO;
using UnityEngine.UI;
using NatML.Recorders;
using NatML.Recorders.Clocks;
using System.Collections;
using UnityEngine.Android;
using TMPro;
using System.Collections.Generic;
using System;

public class VideoCaptureMobile : BaseVideoCapture
{
    [SerializeField]
    private GameObject _processingProgressUI;
    [SerializeField]
    private Image _progressBar;
    [SerializeField]
    private string _gameIdentifier;
    [SerializeField]
    private Documento _compilationDocumentLogger;

    [Header("Display Data")]
    private WebCamDevice[] cam_devices;
    private bool hasCameraPermission = false;
    private WebCamTexture webcamTexture;

    [Header("Video Saver Data")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int frames;

    private MP4Recorder recorder;
    private Coroutine recordVideoCoroutine;

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
                List<string> camNames = new List<string>();
                foreach (WebCamDevice webCamDevice in cam_devices)
                {
                    camNames.Add(webCamDevice.name.ToString());
                }
                if (camNames.Count >= 1)
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
        // Create a recorder
        recorder = new MP4Recorder(width: width, height: height, frameRate: frames);

        //Start recording
        recordVideoCoroutine = StartCoroutine(recording());
        
        onStartRecording?.Invoke();
    }

    private IEnumerator recording()
    {
        var clock = new RealtimeClock();

        while (true)
        {
            // Commit video frame
            recorder.CommitFrame(webcamTexture.GetPixels32(), clock.timestamp);
            yield return new WaitForEndOfFrame();
        }
    }

    protected override void StopVideoCapture()
    {
        stopRecording();
    }


    public async void stopRecording()
    {
        string playerName = PlayerPrefs.GetString(MainMenuController.NAME, "");

        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Videos")))
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Videos"));

        _path = Path.Combine(Application.persistentDataPath, $"Videos/{playerName}_{_gameIdentifier}.mp4");

        //Stop Coroutine
        StopCoroutine(recordVideoCoroutine);

        // Finish writing
        var recordingPath = await recorder.FinishWriting();

        //NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(recordingPath, _path, (success, path) => Debug.Log("Media save result: " + success + " " + path));

        onEndRecording?.Invoke(_path);
        _processingProgressUI.SetActive(true);
    }
}