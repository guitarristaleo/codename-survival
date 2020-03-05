using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectCloner : MonoBehaviour
{
    Dictionary<ScriptableObject, ScriptableObject> Variables = new Dictionary<ScriptableObject, ScriptableObject>();

    private void RegisterVariable(ScriptableObject so) 
    {
        if(!Variables.ContainsKey(so))
            Variables.Add(so, UnityEngine.Object.Instantiate(so));
    }

    public ScriptableObject GetLocalToPrefab(ScriptableObject so)
    {
        RegisterVariable(so);
        return Variables[so];
    }
}
