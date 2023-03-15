using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListenerBase<T> : MonoBehaviour
{
    [Tooltip("Game Event to listen")]
    public GameEventSOBase<T> gameEvent;
    [Tooltip("Actions to perform when triggered")]
    public UnityEvent<T> response;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }
    private void OnDisable()
    {
        gameEvent.Unregister(this);
    }

    public void Respond(T param)
    {
        response.Invoke(param);
    }
}