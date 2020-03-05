using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    [SerializeField] private State InitialState = default;
    private State CurrentState;
    private bool aiActive = true; //TODO: Siento que esto vale para algo, pero de momento no sé cómo aplicarlo
    
    public bool DrawGizmos = true;
    [Tooltip("A position for showing the IA's current state as a coloured circle")]
    [SerializeField] private Transform StateIndicator = default;
    private Color DefaultGizmoColor = Color.grey;
    public float lookSphereCastRadius = 0.1f;

    private void Awake()
    {
        CurrentState = InitialState;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!aiActive)
            return;

        CurrentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (!DrawGizmos)
            return;

        Gizmos.color = CurrentState?.SceneGizmoColor ?? DefaultGizmoColor;
        Gizmos.DrawSphere(StateIndicator.position, lookSphereCastRadius);
    }

    public void TransitionToState(State nextState)
    {
        CurrentState = nextState;
    }

}
