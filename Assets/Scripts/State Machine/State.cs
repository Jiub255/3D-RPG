using UnityEngine;

public abstract class State<T> where T : MonoBehaviour
{
    protected T _runner;

    public State(T runner)
    {
        _runner = runner;
    }

/*    public virtual void Init(T parent)
    {
        _runner = parent;
    }*/

    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
}