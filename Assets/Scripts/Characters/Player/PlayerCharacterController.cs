using UnityEngine;

// Put "Any State" stuff in here?
public class PlayerCharacterController : StateRunner<PlayerCharacterController>, IKnockbackable
{
    public Animator Animator { get; private set; }

    protected override void Awake()
    {
        // Need to get references before calling base.Awake(). 
        Animator = GetComponentInChildren<Animator>();

        base.Awake();
    }

    public void GetKnockedBack(Vector3 knockbackVector)
    {
        Debug.Log("GetKnockedBack called on PlayerCharacterController");
    }
}