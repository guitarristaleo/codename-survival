// ----------------------------------------------------------------------------
// Author:  Marco Uceda López
// Date:    21/01/2020 (Under construction)
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SriptableObjectUtilities/SystemData/MovementSystemData", order = 53)]
public class MovementSystemData : ScriptableObject
{
    [Header("Movement")]
    [Space]

    [Tooltip("Speed of the character for horizontal movment")]
    public float runSpeed = 45f;

    [Tooltip("Amount of maxSpeed applied to crouching movement. 1 = 100%")]
    [Range(0, 1)]
    public float crouchingSpeedModifier = 0.36f;

    [Tooltip("How much to smooth out the movement")]
    [Range(0, 0.3f)]
    public float smoothTime = 0.05f;

    [Header("Jump")]
    [Space]

    [Tooltip("Amount of force added when the player jumps.")]
    public float jumpForce = 50f;

    [Tooltip("Time the player can hold the jump button to extend the jump")]
    public float jumpTime = 0.35f;
}
