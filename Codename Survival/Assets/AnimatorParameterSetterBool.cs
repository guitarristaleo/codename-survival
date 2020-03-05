// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author:  Ryan Hipple
// Date:    04/10/2017
// Adapted: Marco Uceda López 21/01/2020
// ----------------------------------------------------------------------------

using UnityEngine;

public class AnimatorParameterSetterBool : MonoBehaviour
{
    [Tooltip("Name of the parameter to set with the value of Variable.")]
    public string ParameterName;

    [Tooltip("Variable to read from and send to the Animator as the specified parameter.")]
    public BoolVariable Variable;

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
        Animator = GetComponentInParent<Animator>();
    }

    private void OnValidate()
    {
        parameterHash = Animator.StringToHash(ParameterName);
    }

    private void Update()
    {
        Animator.SetBool(parameterHash, Variable.Value);
    }
}