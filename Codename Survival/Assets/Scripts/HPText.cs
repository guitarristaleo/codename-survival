using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPText : MonoBehaviour
{
    public FloatVariable CurrentHP;
    public ScriptableObjectCloner soCloner = default;
    public TextMeshProUGUI TextMesh;

    private void Awake()
    {
        this.CurrentHP = (FloatVariable)soCloner?.GetLocalToPrefab(CurrentHP) ?? this.CurrentHP;
    }

    private void Start()
    {
        TextMesh.text = "Health: " + CurrentHP.Value;
    }

    private void Update()
    {
        TextMesh.text = "Health: " + CurrentHP.Value;
    }
}
