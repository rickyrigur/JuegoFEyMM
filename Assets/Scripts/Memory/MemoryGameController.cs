using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryGameController : MonoBehaviour
{

    public bool exitAudioPlayed;
    public bool videoProcessed;
    public AudioClipSO EndGameAudio;
    public void FinalizarJuego()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        EndGameAudio.Play();
        yield return new WaitForSeconds(EndGameAudio.Lenght);
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
