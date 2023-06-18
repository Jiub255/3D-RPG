using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateRunner<T> : MonoBehaviour where T : MonoBehaviour
{
	[SerializeField]
	private List<SOState<T>> _states;
	private readonly Dictionary<Type, SOState<T>> _stateByType = new();
	private SOState<T> _activeState;

	protected virtual void Awake()
    {
        // Add states to <type, SOState> dictionary. Used for changing states by type in ChangeState. 
		_states.ForEach(s => _stateByType.Add(s.GetType(), s));

        // Default state is first on the list. 
		ChangeState(_states[0].GetType());
    }

    private void Update()
    {
        _activeState.CaptureInput();
		_activeState.Update();
		_activeState.CheckForStateChangeConditions();
    }

    private void FixedUpdate()
    {
        _activeState.FixedUpdate();
    }

	public void ChangeState(Type newStateType)
    {
		if (_activeState != null)
        {
			_activeState.Exit();
        }

		_activeState = _stateByType[newStateType];
		_activeState.Init(GetComponent<T>());
    }
}