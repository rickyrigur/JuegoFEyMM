using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AU_Audio", menuName = "Scriptables/Audio/AudioClip", order = 60)]
public class AudioClipSO : ScriptableObject
{
    [SerializeField, Tooltip("Audio clip description")]
    private string _description = default;
    [SerializeField, Tooltip("Audio clip to use")]
    private AudioClip _clip = default;
    [Range(0, 1), SerializeField, Tooltip("Audio clip volume")]
    private float _volume = 1;

    public AudioClip Clip { get { return _clip; } }
    public float Volume { get { return _volume; } }

    public void Play() => FindObjectOfType<AudioManager>().PlayAudio(this);
}
