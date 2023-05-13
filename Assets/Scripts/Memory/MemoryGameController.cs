using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryGameController : MonoBehaviour
{
    public AudioClipSO EndGameAudio;
    public void FinalizarJuego()
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()
    {
        EndGameAudio.Play();
        yield return new WaitForSeconds(EndGameAudio.Lenght);
        SceneManager.LoadScene(0);
    }
}
