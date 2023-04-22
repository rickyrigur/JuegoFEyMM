using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audiosEnsayo;
    private AudioSource _audioEnsayo;

    void Start()
    {
        _audioEnsayo = GetComponent<AudioSource>();
    }

    public void CargarAudio(int audio)
    {
        _audioEnsayo.clip = audiosEnsayo[audio];
    }

    public void EmpezarAudio()
    {
        _audioEnsayo.Play();
    }

    public void PararAudio()
    {
        _audioEnsayo.Stop();
    }

    public float TiempoAudio()
    {
        return _audioEnsayo.clip.length;
    }

    public bool EstaReproduciendo() 
    {
        return _audioEnsayo.isPlaying;
    }

    public void PlayAudio(AudioClipSO audio)
    {
        _audioEnsayo.clip = audio.Clip;
        _audioEnsayo.volume = audio.Volume;
        _audioEnsayo.Play();
    }
    
}
