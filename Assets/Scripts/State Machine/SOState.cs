using UnityEngine;

public abstract class SOState<T> : ScriptableObject where T : MonoBehaviour
{
	protected T _runner;

	public virtual void Init(T parent)
    {
        _runner = parent;
    }

    public abstract void CaptureInput();
    public abstract void Update();
    // Use _runner.SetState() to change states. 
    public abstract void CheckForStateChangeConditions();
    public abstract void FixedUpdate();
    public abstract void Exit();
}