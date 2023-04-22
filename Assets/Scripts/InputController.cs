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

        if (Input.GetMouseButton(0))
        {
            OnMaintain?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnRelease?.Invoke();
        }
    }

    public bool TrackInput { set { _trackInput = value; } }
}