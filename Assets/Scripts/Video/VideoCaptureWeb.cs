using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEngine.Windows.WebCam;
#endif

public class VideoCaptureWeb : BaseVideoCapture
{
#if UNITY_EDITOR
    private VideoCapture m_VideoCapture = null;
#endif

    protected override void StartVideoCapture()
    {

#if UNITY_EDITOR

        Resolution cameraResolution = VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        float cameraFramerate = VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();

        VideoCapture.CreateAsync(false, delegate (VideoCapture videoCapture)
        {
            if (videoCapture != null)
            {
                m_VideoCapture = videoCapture;
                CameraParameters cameraParameters = new CameraParameters();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate = cameraFramerate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

                m_VideoCapture.StartVideoModeAsync(cameraParameters,
                    VideoCapture.AudioState.ApplicationAndMicAudio,
                    OnStartedVideoCaptureMode);
            }
            else
            {
                Debug.LogError("Failed to create VideoCapture Instance!");
            }
        });
#endif
    }

    protected override void StopVideoCapture()
    {
#if UNITY_EDITOR
        m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
#endif
    }

    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
#if UNITY_EDITOR
        SetPath();
        m_VideoCapture.StartRecordingAsync(_path, OnStartedRecordingVideo);
#endif
    }

    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
#if UNITY_EDITOR
        onStartRecording?.Invoke();
#endif
    }

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
#if UNITY_EDITOR
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
#endif
    }

    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
#if UNITY_EDITOR
        onEndRecording?.Invoke(_path);
#endif
    }
}
