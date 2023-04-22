using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class gameManager : MonoBehaviour
{
    public GameObject[] cartas;

    public Basket canastaL;
    public Basket canastaR;

    public Transform carta;
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

    private AudioManager audioManager;
    private bool _incorrecto;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        OnGameLoad?.Invoke();
    }

    public void OnPress()
    {
        carta = null;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.tag != "Canasta" && hit.collider.gameObject.tag != "Muestra")
        {
            carta = hit.collider.gameObject.GetComponent<Transform>();
        }
    }

    public void OnMaintain()
    {
        Vector2 MousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(MousePosition);
        if (carta != null)
        {
            carta.transform.position = objPosition;
        }
    }

    public void OnRelease()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject.tag != "Canasta" && hit.collider.gameObject.tag != "Muestra" && carta != null)
        {
            if (hit.collider.OverlapPoint(canastaL.transform.position))
            {
                if (canastaL.Validate(carta.GetComponent<Carta>()))
                    OnCorrect?.Invoke();
                else
                {
                    _incorrecto = true;
                    OnIncorrect?.Invoke();
                }

                carta.GetComponent<Collider2D>().enabled = false;
                carta.transform.localScale = new Vector3(0.2f, 0.2f, 0);
                OnCardPlaced?.Invoke();
            }

            else if (hit.collider.OverlapPoint(canastaR.transform.position))
            {
                if (canastaR.Validate(carta.GetComponent<Carta>()))
                    OnCorrect?.Invoke();
                else
                {
                    _incorrecto = true;
                    OnIncorrect?.Invoke();
                }

                carta.GetComponent<Collider2D>().enabled = false;
                carta.transform.localScale = new Vector3(0.2f, 0.2f, 0);
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
            yield return new WaitForSeconds(audioManager.TiempoAudio());
            OnIntroEnds?.Invoke();

            Vector3 endScale = new Vector3(2.5f, 2.5f, 0);

            yield return new WaitForSeconds(6f);
            canastaL.AnimateScale(endScale, 2);
            yield return new WaitForSeconds(3f);
            canastaR.AnimateScale(endScale, 2);
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
        yield return new WaitForSeconds(audioManager.TiempoAudio());
        OnSecondLevelIntroEnds?.Invoke();

        Vector3 endScale = new Vector3(2.5f, 2.5f, 0);
        yield return new WaitForSeconds(7f);
        canastaR.AnimateScale(endScale, 2);

        yield return new WaitForSeconds(4f);
        canastaL.AnimateScale(endScale, 2);
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
        yield return new WaitForSeconds(audioManager.TiempoAudio() + 1f);
        OnLevel7IntroEnds?.Invoke();
    }

    IEnumerator NivelOchoInicioSubnivel()
    {
        yield return new WaitForSeconds(audioManager.TiempoAudio() + 1f);
        OnLevel8IntroEnds?.Invoke();
    }

    public void FinalizarJuego()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }  
}
