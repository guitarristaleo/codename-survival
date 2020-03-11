using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSystem : MonoBehaviour
{
    public void DisableCharacter()
    {
        this.gameObject.GetComponentInParent<ScriptableObjectCloner>().gameObject.layer = LayerMask.NameToLayer("DeadCharacter");
    }
}
