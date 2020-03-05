using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    [Tooltip("A position marking where to check if the character is grounded")]
    [SerializeField] private Transform groundCheck = default;

    [Tooltip("A mask determining what is ground to the character")]
    public LayerMask whatIsGround;

    [Tooltip("Radius of the overlap circle to determine if grounded")]
    const float k_GroundedRadius = 0.2f;

    [Tooltip("Whether or not the character is grounded")]
    [SerializeField] private BoolVariable isGrounded = default;


    [Header("Events")]
    [Space]

    [Tooltip("Event that is triggered when the character lands on ground")]
    public UnityEvent OnLandEvent;

    [Tooltip("Event that is triggered when the character stops being grounded")]
    public UnityEvent OnLandLeftEvent;

    /// <summary>
    /// Indicates if the player is on the ground.
    /// </summary>
    public bool IsGrounded { get => isGrounded.Value; }

    private void Awake()
    {
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        OnLandEvent = OnLandEvent ?? new UnityEvent();
        OnLandLeftEvent = OnLandLeftEvent ?? new UnityEvent();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
    }

    private void CheckIfGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded.SetValue(false);

        // The character is grounded if a circlecast to the groundCheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                // If we are here, we are colliding with some ground
                isGrounded.SetValue(true);
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        // If the character isn't grounded but was grounded before, invoke an event telling the character has left ground.
        if (!isGrounded && wasGrounded)
        {
            OnLandLeftEvent.Invoke();
        }
    }

}