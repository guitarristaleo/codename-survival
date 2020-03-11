// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;


public class GameEventListener : MonoBehaviour, IGameEventListener
{
    private ScriptableObjectCloner soCloner = default;

    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Whether the event is local to the GameObject or it is global to the game")]
    public bool GetFromLocalEvents = true;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    private void Awake()
    {
        this.soCloner = GetComponentInParent<ScriptableObjectCloner>();
        if (GetFromLocalEvents)
        {
            this.Event = (GameEvent)soCloner?.GetLocalToPrefab(Event) ?? this.Event;
        }
    }

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
