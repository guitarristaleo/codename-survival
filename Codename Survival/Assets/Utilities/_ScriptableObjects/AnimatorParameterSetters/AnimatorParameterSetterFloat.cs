// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author:  Ryan Hipple
// Date:    04/10/2017
// Adapted: Marco Uceda López 21/01/2020
// ----------------------------------------------------------------------------

using UnityEngine;

public class AnimatorParameterSetterFloat : MonoBehaviour
{
    private ScriptableObjectCloner soCloner = default;

    [Tooltip("Name of the parameter to set with the value of Variable.")]
    public string ParameterName;

    [Tooltip("Variable to read from and send to the Animator as the specified parameter.")]
    public FloatVariable Variable;

    [Tooltip("If checked, the animator will receive the absolute value of the variable.")]
    public bool ApplyAsAbsoluteValue = false;

    /// <summary>
    /// Animator to set parameters on.
    /// </summary>
    private Animator Animator;

    /// <summary>
    /// Value to be applied on the animator.
    /// </summary>
    private float AnimatorValue;

    /// <summary>
    /// Animator Hash of ParameterName, automatically generated.
    /// </summary>
    private int parameterHash;

    private void Awake()
    {
        this.soCloner = GetComponentInParent<ScriptableObjectCloner>();
        this.Variable = (FloatVariable)soCloner?.GetLocalToPrefab(Variable) ?? this.Variable;
        Animator = GetComponentInParent<Animator>();
        parameterHash = Animator.StringToHash(ParameterName);
    }

    private void OnValidate()
    {
        parameterHash = Animator.StringToHash(ParameterName);
    }

    private void Update()
    {
        AnimatorValue = Variable.Value;

        if (ApplyAsAbsoluteValue)
            AnimatorValue = Mathf.Abs(AnimatorValue);

        Animator.SetFloat(parameterHash, AnimatorValue);
    }
}
