using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onCorrect;

    [SerializeField]
    private UnityEvent _onFail;

    [SerializeField]
    private UnityEvent _onInterrupt;

    [SerializeField]
    private AudioClipSO _correctAudio;

    [SerializeField]
    private AudioClipSO _failAudio;

    private int wrongAmount = 0;

    private AudioManager _audioManager => FindObjectOfType<AudioManager>();

    private void Start()
    {
        GameVars.PlayingTutorial = true;
    }

    public void Validate(bool result)
    {
        if (result)
        {
            StartCoroutine(PlayAudiosAndValidate(_correctAudio, _onCorrect));
            GameVars.PlayingTutorial = false;
        }
        else
        {
            wrongAmount++;
            if (wrongAmount < 3)
            {
                StartCoroutine(PlayAudiosAndValidate(_failAudio, _onFail));
            }
            else
            {
                _onInterrupt?.Invoke();
            }
        }
    }

    IEnumerator PlayAudiosAndValidate(AudioClipSO clip, UnityEvent callback)
    {
        clip.Play();
        yield return new WaitWhile(() => _audioManager.EstaReproduciendo());
        callback?.Invoke();
    }
}
