/*using UnityEngine;
using System.Linq;
using UnityEngine.Windows.WebCam;

public class VideoCaptureWeb : BaseVideoCapture
{
    private VideoCapture m_VideoCapture = null;

    protected override void StartVideoCapture()
    {
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
    }

    protected override void StopVideoCapture()
    {
        m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
    }

    void OnStartedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        SetPath();
        m_VideoCapture.StartRecordingAsync(_path, OnStartedRecordingVideo);
    }

    void OnStartedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        onStartRecording?.Invoke();
    }

    void OnStoppedRecordingVideo(VideoCapture.VideoCaptureResult result)
    {
        m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
    }

    void OnStoppedVideoCaptureMode(VideoCapture.VideoCaptureResult result)
    {
        onEndRecording?.Invoke(_path);
    }
}
*/