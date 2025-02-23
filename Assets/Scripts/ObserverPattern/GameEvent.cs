using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//{} ()
public abstract class GameEvent<T> : ScriptableObject
{
    private List<GameEventListener<T>> _eventListeners = new List<GameEventListener<T>>();

    public void Invoke(T data)
    {
        foreach (GameEventListener<T> listener in _eventListeners)
        {
            listener.OnEventInvoked(data);

        }
    }

    public void RegisterListener(GameEventListener<T> listener) 
    {
        _eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener<T> listener)
    {
        _eventListeners.Remove(listener);
    }
  
}


