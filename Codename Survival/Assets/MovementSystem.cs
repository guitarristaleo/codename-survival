// ----------------------------------------------------------------------------
// Author:  Marco Uceda López
// Date:    21/01/2020 (Under construction)
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystem : MonoBehaviour
{
    // Components
    private GroundChecker MyGroundChecker;      //Component used to check whether the character is grounded
    private Rigidbody2D MyRigidbody2D;          // RigidBody component which will be move with this script

    // Scriptable Object Resources
    public MovementSystemData MovementSystemData = default;
    public FloatVariable PlayerInput_HorizontalMovement;
    public BoolVariable PlayerInput_JumpPressed;
    public FloatVariable Character_HorizontalSpeed;
    public BoolVariable isJumping;             // Indicates if the player is in the middle of a jump.

    // Private variables for internal use
    //private float horizontalMove = 0f; //TODO: (Revisar) Sustituimos esta variable interna por un FloatVariable (Character_HorizontalSpeed) para comunicar con el animador
    private Vector3 m_Velocity = Vector3.zero;  // currentVelocity parameter of Vector3.SmoothDamp when smoothing the character's movement
    private bool facingRight = true;            // For determining which way the player is currently facing.
    private bool crouching = false;             // For determining if the player must be crouching or not
                                                //private bool standing = true;               // For determining if the player is standing, or at least trying to

    private float jumpTimeCounter;              // Indicates the time remaning for the character to extend its jump.

    // Constants for internal use
    private const float k_SpeedModifierBase = 10f;

    private void Awake()
    {
        MyRigidbody2D = GetComponentInParent<Rigidbody2D>();
        MyGroundChecker = GetComponentInChildren<GroundChecker>();
    }

    private void FixedUpdate()
    {
        Move(PlayerInput_HorizontalMovement.Value);

        if (PlayerInput_JumpPressed.Value)
            JumpExtended();
        else
            isJumping.SetValue(false);
    }

    #region HorizontalMovement

    public void Move(float horizontalInput)
    {
        // Only control the player if it has no impediment
        if (!CanMove())
            return;

        // We take into account the horizontal input value, the character's movement speed, any speed modifiers like crouching and the delta time.
        Character_HorizontalSpeed.SetValue(horizontalInput * MovementSystemData.runSpeed * GetSpeedModifier() * Time.fixedDeltaTime);

        // Move the character by finding the target velocity
        Vector3 targetPosition = new Vector2(Character_HorizontalSpeed.Value, MyRigidbody2D.velocity.y);

        // And then smoothing it out and applying it to the character
        MyRigidbody2D.velocity = Vector3.SmoothDamp(
            MyRigidbody2D.velocity  // current
            , targetPosition        // target
            , ref m_Velocity        // ref Vector3 currentVelocity
            , MovementSystemData.smoothTime);          // float smoothTime

        if (Character_HorizontalSpeed.Value > 0 && !facingRight || Character_HorizontalSpeed.Value < 0 && facingRight)
        {
            Flip();
        }
    }

    private bool CanMove()
    {
        // For now, the character can move if it is either grounded or if it has air-control
        return true;
    }

    private float GetSpeedModifier()
    {
        float speedModifier;

        // Reset the modifier to its neutral value
        speedModifier = k_SpeedModifierBase;

        if (crouching)
        {
            // Reduce the speed by the crouchSpeed modifier
            speedModifier *= MovementSystemData.crouchingSpeedModifier;
        }

        return speedModifier;
    }

    #endregion

    #region Jumping

    public void Jump()
    {
        /*
        Para el animador:
        El personaje inicia el salto, lo extiende o no, cae y llega a suelo.
            *El personaje inicia el salto:
                InputSystem registra OnJump() y llama a Jump() en MovementSystem
                Jump, si CanJump() registra la variable isJumping a true. Esa es la señal para pasar de Idle/Run a Jump
            *lo extiende o no:
                podría... en lugar de hacer un tap = jump y hold = jumpextended, simplemente hold = jump, y tratar dentro las dos cosas?
                pero vamos a pensar en tap + hold
                ohhh. Jump() activa el timer, so... aquí no hago nada realmente, sino en "cae"
            *cae
                o bien el timer de salto ha llegado a 0, o bien he dejado de mandar la señal de hold jump.
                    
            *llega al suelo
        */
        // If the player should jump...
        if (CanJump())
        {
            // For initializing the jump, apply an inicial impulse force and start counting time for the jump to be extended
            isJumping.SetValue(true);
            jumpTimeCounter = MovementSystemData.jumpTime;
            MyRigidbody2D.AddForce(new Vector2(0f, MovementSystemData.jumpForce), ForceMode2D.Impulse);
        } else
        {
            isJumping.SetValue(false);
        }
    }

    public void JumpExtended()
    {
        // If jump is still pressed and the character can extend their jump, they do
        if (CanExtendJump())
        {
            MyRigidbody2D.velocity += Vector2.up * MovementSystemData.jumpForce * Time.deltaTime;
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping.SetValue(false);
        }
    }

    private bool CanJump()
    {
        bool canJump = false;

        if (MyGroundChecker.IsGrounded)
            canJump = true;

        return canJump;
    }

    private bool CanExtendJump()
    {
        bool canJump = false;

        if (isJumping && jumpTimeCounter > 0)
            canJump = true;

        return canJump;
    }

    #endregion

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = MyRigidbody2D.transform.localScale;
        theScale.x *= -1;
        MyRigidbody2D.transform.localScale = theScale;
    }
}
