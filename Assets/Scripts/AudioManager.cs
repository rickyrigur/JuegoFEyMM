using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audiosEnsayo;
    AudioSource audioEnsayo = new AudioSource();

    void Start()
    {
        audioEnsayo = this.GetComponent<AudioSource>();
    }

    public void CargarAudio(int audio)
    {
        audioEnsayo.clip = audiosEnsayo[audio];
    }

    public void EmpezarAudio()
    {
        audioEnsayo.Play();
    }

    public void PararAudio()
    {
        audioEnsayo.Stop();
    }

    public float TiempoAudio()
    {
        return audioEnsayo.clip.length;
    }
    
}
