using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject[] cartas;

    public Basket canastaL;
    public Basket canastaR;

    public GameObject opciones;
    
    public UnityEvent OnGameLoad;
    
    public UnityEvent OnIntroStarts;
    public UnityEvent OnIntroEnds;
    
    public UnityEvent OnCardPlaced;
    public UnityEvent OnCorrect;
    public UnityEvent OnIncorrect;

    public UnityEvent OnReplayTutorial;
    public UnityEvent OnEndTutorial;
    public UnityEvent OnSecondLevelIntroEnds;
    public UnityEvent OnLevel7IntroEnds;
    public UnityEvent OnLevel8IntroEnds;

    public AudioClipSO EndGameAudio;

    private AudioManager audioManager;
    private bool _incorrecto;

    public bool exitAudioPlayed;
    public bool videoProcessed;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        OnGameLoad?.Invoke();
    }

    public void ValidatePointer(Vector2 position, Carta card)
    {
        Collider2D cardCollider = card.GetComponent<Collider2D>();
        Collider2D basketLCollider = canastaL.GetComponent<Collider2D>();
        Collider2D basketRCollider = canastaR.GetComponent<Collider2D>();

        List<Collider2D> res = new List<Collider2D>();
        if (cardCollider.OverlapCollider(new ContactFilter2D(), res) > 0)
        {
            if (res.Contains(basketLCollider))
            {
                if (canastaL.Validate(card))
                    OnCorrect?.Invoke();
                else
                {
                    _incorrecto = true;
                    OnIncorrect?.Invoke();
                }

                cardCollider.enabled = false;
                card.DeactivateDrag();
                card.transform.localScale = new Vector3(0.6f, 0.6f, 0);
                OnCardPlaced?.Invoke();
            }
            else if (res.Contains(basketRCollider))
            {
                if (canastaR.Validate(card))
                    OnCorrect?.Invoke();
                else
                {
                    _incorrecto = true;
                    OnIncorrect?.Invoke();
                }

                cardCollider.enabled = false;
                card.DeactivateDrag();
                card.transform.localScale = new Vector3(0.6f, 0.6f, 0);
                OnCardPlaced?.Invoke();
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        if(!audioManager.EstaReproduciendo())
        {
            OnIntroStarts?.Invoke();
            yield return new WaitWhile(() => audioManager.EstaReproduciendo());
            OnIntroEnds?.Invoke();

            yield return new WaitForSeconds(6f);
            canastaL.AnimateScale();
            yield return new WaitForSeconds(3f);
            canastaR.AnimateScale();
            yield return new WaitForSeconds(2f);
        }        
    }

    public void EndTutorial()
    {
        if (_incorrecto)
        {
            _incorrecto = false;
            OnReplayTutorial?.Invoke();
        }
        else
        {
            OnEndTutorial?.Invoke();
            StartCoroutine(PlaySecondLevel());
        }
    }
    
    IEnumerator PlaySecondLevel()
    {
        yield return new WaitWhile(() => audioManager.EstaReproduciendo());
        OnSecondLevelIntroEnds?.Invoke();

        yield return new WaitForSeconds(7f);
        canastaR.AnimateScale();
        yield return new WaitForSeconds(4f);
        canastaL.AnimateScale();
    }

    public void EndLevel7Training()
    {
        StartCoroutine(NivelSieteInicioSubnivel());
    }

    public void EndLevel8Training()
    {
        StartCoroutine(NivelOchoInicioSubnivel());
    }

    IEnumerator NivelSieteInicioSubnivel()
    {
        yield return new WaitWhile(() => audioManager.EstaReproduciendo());
        OnLevel7IntroEnds?.Invoke();
    }

    IEnumerator NivelOchoInicioSubnivel()
    {
        yield return new WaitWhile(() => audioManager.EstaReproduciendo());
        OnLevel8IntroEnds?.Invoke();
    }

    public void FinalizarJuego()
    {
        StartCoroutine(EndGame());
    }
    
    IEnumerator EndGame()
    {
        EndGameAudio.Play();
        yield return new WaitWhile(() => audioManager.EstaReproduciendo());
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
