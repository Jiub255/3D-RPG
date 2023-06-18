using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Want to be able to automatically unsubscribe events instead of having to setup OnDisable in every class. 
// Not sure how though, or if it's possible. 
public class AutoUnregisterMonoBehaviour : MonoBehaviour
{
    // But how to deal with actions with parameters? And that's assuming this even works. 
/*    private List<Action> _actions = new();

    private void RegisterEvent(Action a, Action m)
    {
        a += m;
        _actions.Add(a);
    }

    private void OnDisable()
    {
        foreach (Action a in _actions)
        {
            ClearAllDelegates(a);
        }
    }

    private void ClearAllDelegates(Action e)
    {
        foreach (Delegate d in e.GetInvocationList())
        {
            e -= (Action)d;
        }
    }*/
}