using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum EventType
{
    LoadLevel,
    ResetLevel,
    CompleteLevel,
    NextLevel,
    Lose,
}
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    private void Awake()
    {
        instance = this;
    }

    public event Action<EventType> OnEventEmitted;

    private bool enableResetLevel = true;

    private void EmitEvent(EventType eventType)
    {
        OnEventEmitted?.Invoke(eventType);
    }

    public void EmitNextLevelEvent()
    {
        enableResetLevel = true;
        EmitEvent(EventType.NextLevel);
    }

    public void EmitCompleteLevelEvent()
    {
        enableResetLevel = false;
        EmitEvent(EventType.CompleteLevel);
    }

    public void EmitLoadLevelEvent()
    {
        if (!enableResetLevel)
        {
            return;
        }
        EmitEvent(EventType.LoadLevel);
    }
}
