// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author:  Ryan Hipple
// Date:    04/10/2017
// Adapted: Marco Uceda López 21/01/2020
// ----------------------------------------------------------------------------

using UnityEngine;

public class AnimatorParameterSetterTrigger : MonoBehaviour, IGameEventListener
{
    private ScriptableObjectCloner soCloner = default;

    [Tooltip("Name of the parameter to set with the value of Variable.")]
    public string ParameterName;

    [Tooltip("Variable to read from and send to the Animator as the specified parameter.")]
    public GameEvent Event;


    /// <summary>
    /// Animator to set parameters on.
    /// </summary>
    private Animator Animator;

    /// <summary>
    /// Animator Hash of ParameterName, automatically generated.
    /// </summary>
    private int parameterHash;

    private void Awake()
    {
        this.soCloner = GetComponentInParent<ScriptableObjectCloner>();
        this.Event = (GameEvent)soCloner?.GetLocalToPrefab(Event) ?? this.Event;
        Animator = GetComponentInParent<Animator>();
        parameterHash = Animator.StringToHash(ParameterName);
    }

    private void OnValidate()
    {
        parameterHash = Animator.StringToHash(ParameterName);
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
		Animator.SetTrigger(parameterHash);
	}
}
