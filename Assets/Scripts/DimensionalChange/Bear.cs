using UnityEngine;
using UnityEngine.Events;

public class Bear : MonoBehaviour
{
    public UnityEvent OnFirstClick;
    public UnityEvent OnSecondClick;

    private bool _alreadyPressed;
    private bool _enabled = true;
    public void OnClick()
    {
        if (!_enabled)
            return;

        if (_alreadyPressed)
        {
            OnSecondClick?.Invoke();
        }
        else
        {
            _alreadyPressed = true;
            OnFirstClick?.Invoke();
        }
    }

    public bool EnableBear { set { _enabled = value; } }
}
