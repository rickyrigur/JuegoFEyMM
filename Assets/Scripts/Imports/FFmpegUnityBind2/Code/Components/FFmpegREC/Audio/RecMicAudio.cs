using UnityEngine;

namespace FFmpegUnityBind2.Components
{
    class RecMicAudio : MonoBehaviour, IRecAudio
    {
        [SerializeField]
        int maxLength = 60;

        AudioClip buffer;

        public void StartRecording()
        {
            maxLength = 1800;
            buffer = Microphone.Start(null, false, maxLength, AudioSettings.outputSampleRate);
        }

        public void StopRecording(string savePath)
        {
            Microphone.End(null);
            WAV.Save(savePath, buffer);
        }
    }
}