using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventSOBase<T> : ScriptableObject
{
    private List<GameEventListenerBase<T>> _listeners = new List<GameEventListenerBase<T>>();

    public void Happen(T param)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Respond(param);
    }
    public void Register(GameEventListenerBase<T> listener)
    {
        if (!_listeners.Contains(listener)) _listeners.Add(listener);
    }
    public void Unregister(GameEventListenerBase<T> listener)
    {
        if (_listeners.Contains(listener)) _listeners.Remove(listener);
    }
}