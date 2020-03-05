using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class ControllerSystem : MonoBehaviour
{
    public GameObject Character = default;
    private MovementSystem MyMovementSystem = default;
    private CombatSystem MyCombatSystem = default;

    private float m_Move;
    private bool m_Attack;

    private void Awake()
    {
        MyMovementSystem = Character.GetComponentInChildren<MovementSystem>();
        MyCombatSystem = Character.GetComponentInChildren<CombatSystem>();
    }

    #region MovementSystem

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<float>();
        MyMovementSystem.PlayerInput_HorizontalMovement.SetValue(m_Move);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MyMovementSystem.Jump();
            MyMovementSystem.PlayerInput_JumpPressed.SetValue(true);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            MyMovementSystem.PlayerInput_JumpPressed.SetValue(false);
        }
        //if (context.interaction == InputInter)
    }

    #endregion

    #region CombatSystem

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MyCombatSystem.Attack();
        }
    }

    public void OnChargedAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MyCombatSystem.ChargedAttack();
        }
    }

    #endregion

}
