// ----------------------------------------------------------------------------
// Author:  Marco Uceda López
// Date:    21/01/2020 (Under construction)
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SriptableObjectUtilities/SystemData/CombatSystemData", order = 53)]
public class CombatSystemData : ScriptableObject
{
    /// <summary>
    /// Range of the attack
    /// </summary>
    [Tooltip("Range of the attack")]
    public float AttackRange = 0.5f; //Ajustar valor... veremos a ver si lo puedo hacer visualmente bien estando esto en SO. Brackeys lo tiene como variable en el MonoBehaviour

    /// <summary>
    /// Damage that will be dealt to the unit hit
    /// </summary>
    [Tooltip("Damage that will be dealt to the unit hit")]
    public float AttackDamage = 40f; //Ajustar valor

    /// <summary>
    /// Number of attacks that can be performed within a second
    /// </summary>
    [Tooltip("Number of attacks that can be performed within a second")]
    public float AttackRate = 2f;

    /// <summary>
    /// Indicates the layers that will be affected by the attack
    /// </summary>
    [Tooltip("Indicates the layers that will be affected by the attack")]
    public LayerMask EnemyLayers;
}