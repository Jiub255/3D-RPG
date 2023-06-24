using UnityEngine;

public abstract class StateRunner2<T> : MonoBehaviour where T : MonoBehaviour
{
/*	public List<State<T>> States = new();
	private readonly Dictionary<Type, State<T>> _stateByType = new();*/
	protected State<T> _activeState;

	protected virtual void Awake()
    {
    }

    private void Update()
    {
        if (_activeState != null)
        {
            _activeState.Update();
        }
        else
        {
            Debug.LogWarning($"No active state in {name}");
        }
    }

    private void FixedUpdate()
    {
        if (_activeState != null)
        {
            _activeState.FixedUpdate();
        }
        else
        {
            Debug.LogWarning($"No active state in {name}");
        }
    }

    public void ChangeState2(State<T> state)
    {
        if (_activeState != null)
        {
            _activeState.Exit();
        }

        _activeState = state;

//        Debug.Log($"Changed state to: {_activeState.GetType()}");
    }
}