using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target { get; private set; }


    public void Enter(Player target)
    {
        if(enabled == false)
        {
            Target = target;
            enabled = true;

            foreach(Transition transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public void Exit()
    {
        if(enabled == false)
        {
            foreach (Transition transition in _transitions)
            {
                transition.enabled = false;
            }
        
            enabled = false;
        }
    }

    public State GetNext()
    {
        foreach(Transition transition in _transitions)
        {
            if(transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}