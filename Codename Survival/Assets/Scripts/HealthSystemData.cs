// ----------------------------------------------------------------------------
// Author:  Marco Uceda López
// Date:    21/01/2020 (Under construction)
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SriptableObjectUtilities/SystemData/HealthSystemData", order = 53)]
public class HealthSystemData : ScriptableObject
{
    /// <summary>
    /// Health Points value when the character is full HP
    /// </summary>
    [Tooltip("Health Points value when the character is full HP")]
    public float MaxHP;
}