using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] Actions = default;
    public Transition[] Transitions = default;
    public Color SceneGizmoColor;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        ChekTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act(controller);
        }
    }

    private void ChekTransitions(StateController controller)
    {
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool decisionSucceeded = Transitions[i].Decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(Transitions[i].NextState);
            }
        }
    }
}