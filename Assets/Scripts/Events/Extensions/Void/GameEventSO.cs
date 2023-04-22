using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GE_GameEvent", menuName = "Scriptables/Game Event/Game Event", order = 50)]
public class GameEventSO : ScriptableObject
{
    private List<GameEventListener> _listeners = new List<GameEventListener>();

    public void Happen()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].Respond();
    }
    public void Register(GameEventListener listener)
    {
        if (!_listeners.Contains(listener)) _listeners.Add(listener);
    }
    public void Unregister(GameEventListener listener)
    {
        if (_listeners.Contains(listener)) _listeners.Remove(listener);
    }
}
