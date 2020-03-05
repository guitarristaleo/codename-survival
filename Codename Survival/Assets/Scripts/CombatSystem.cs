// ----------------------------------------------------------------------------
// Author:  Marco Uceda López
// Date:    21/01/2020 (Under construction)
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public CombatSystemData CombatSystemData = default;
    public GameEvent Attack_Trigger = default;

    [Tooltip("A position marking where the attack takes place")]
    [SerializeField] private Transform AttackPoint = default;

    /// <summary>
    /// Indicates the moment in time when the character can perform an attack again.
    /// </summary>
    private float NextAttackTime = 0f;

    // Functions

    /// <summary>
    /// Logic to make an Attack.
    /// </summary>
    public void Attack()
    {
        if (!CanAttack())
            return;

        // Play an attack animation
        //Parameter: Attack (Trigger)
        // Transition in: Exit time false; transition duration 0
        // Transition out: any conditions, exit time true, exit time = tiempo que dure la animación
        Attack_Trigger.Raise(); //TODO: Esto está pendiente de cambiar porque igual no tiro de triggers para el ataque, ni para nada. Los triggers parecen muy mierda seca en el animador.

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, CombatSystemData.AttackRange, CombatSystemData.EnemyLayers); 

        // Damage them
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            hitEnemies[i].GetComponentInChildren<HealthSystem>()?.TakeDamage(this.CombatSystemData.AttackDamage);
        }

        // Set the mark of time when another attack can be performed
        NextAttackTime = Time.time + 1f / CombatSystemData.AttackRate;
    }

    /// <summary>
    /// Logic to make a Charged Attack.
    /// </summary>
    public void ChargedAttack()
    {
        Debug.Log("Charging attack...!");
    }

    /// <summary>
    /// Logic to check whether the character can perform an attack.
    /// </summary>
    private bool CanAttack()
    {
        bool canAttack = true;

        if (NextAttackTime >= Time.time)
            canAttack = false;

        return canAttack;
    }

    /// <summary>
    /// MonoBehaviour callback to draw gizmos on the editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, CombatSystemData.AttackRange);
    }
}