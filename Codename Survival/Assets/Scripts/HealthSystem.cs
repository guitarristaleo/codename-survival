// ----------------------------------------------------------------------------
// Author:  Marco Uceda López
// Date:    21/01/2020 (Under construction)
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private ScriptableObjectCloner soCloner = default;
    private bool Dead = false;

    public GameEvent Hit_Event = default;
    public GameEvent Death_Event = default;

    /// <summary>
    /// Health Points value when the character is full HP
    /// </summary>
    [Tooltip("Health Points value when the character is full HP")]
    public FloatVariable CurrentHP;
    //public FloatReference CurrentHP;

    public HealthSystemData HealthSystemData;

    private void Awake()
    {
        this.soCloner = GetComponentInParent<ScriptableObjectCloner>();
        this.CurrentHP = (FloatVariable)soCloner?.GetLocalToPrefab(CurrentHP) ?? this.CurrentHP;
        this.Hit_Event = (GameEvent)soCloner?.GetLocalToPrefab(Hit_Event) ?? this.Hit_Event;
        this.Death_Event = (GameEvent)soCloner?.GetLocalToPrefab(Death_Event) ?? this.Death_Event;
    }

    // Start is called before the first frame update
    void Start()
    {
        RestoreFullHP();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestoreHP(float value)
    {
        CurrentHP.ApplyChange(value);
        CheckMaxHpThreshold();
    }

    public void RestoreFullHP()
    {
        CurrentHP.SetValue(HealthSystemData.MaxHP);
    }

    public void TakeDamage(float value)
    {
        Debug.Log("TakeDamage (" + value + ")");
        CurrentHP.ApplyChange(-value);
        CheckDeathThreshold();
        Hit_Event?.Raise();
    }

    /// <summary>
    /// Prevent the health points from getting above their max value
    /// </summary>
    private void CheckMaxHpThreshold()
    {
        if (CurrentHP.Value > HealthSystemData.MaxHP)
            CurrentHP.SetValue(HealthSystemData.MaxHP);
    }

    /// <summary>
    /// Check if the health points have fallen below 0 to trigger death
    /// </summary>
    private void CheckDeathThreshold()
    {
        if (CurrentHP.Value <= 0 && !Dead)
            Die();
    }

    private void Die()
    {
        Debug.Log("I DIED!!");
        Dead = true;
        Death_Event?.Raise();
    }
}
