using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryGameController : MonoBehaviour
{

    public bool exitAudioPlayed;
    public bool videoProcessed;
    public AudioClipSO EndGameAudio;

    private AudioManager _audiomanager => FindObjectOfType<AudioManager>();
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void FinalizarJuego()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        EndGameAudio.Play();
        yield return new WaitWhile(() => _audiomanager.EstaReproduciendo());
        exitAudioPlayed = true;
        CheckIfCanExit();
    }

    public void VideoProcessed()
    {
        videoProcessed = true;
        CheckIfCanExit();
    }


    public void CheckIfCanExit()
    {
        if (exitAudioPlayed && videoProcessed)
            SceneManager.LoadScene(0);
    }
}
