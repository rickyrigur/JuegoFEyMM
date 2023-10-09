using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public UnityEvent OnPress;
    public UnityEvent OnMaintain;
    public UnityEvent OnRelease;

    private AudioManager _audioManager;
    private bool _trackInput;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (!_trackInput || _audioManager.EstaReproduciendo())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            OnPress?.Invoke();
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            OnRelease?.Invoke();
        }
        else if (Input.GetMouseButton(0))
        {
            OnMaintain?.Invoke();
        }
    }

    public bool TrackInput { set { _trackInput = value; } }
}