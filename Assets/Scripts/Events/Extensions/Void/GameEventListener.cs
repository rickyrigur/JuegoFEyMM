using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("Game Event to listen")]
    public GameEventSO gameEvent;
    [Tooltip("Actions to perform when triggered")]
    public UnityEvent response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }
    private void OnDisable()
    {
        gameEvent.Unregister(this);
    }

    public void Respond()
    {
        response.Invoke();
    }
}
