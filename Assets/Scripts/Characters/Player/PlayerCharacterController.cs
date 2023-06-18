using UnityEngine;

// Put "Any State" stuff in here?
public class PlayerCharacterController : StateRunner<PlayerCharacterController>, IKnockbackable
{
    // TODO - Get needed references here? Or through _runner in the individual states?  
    // Probably just here if multiple states need it, but in the state if it's the only one using it. 
    public Animator Animator { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponentInChildren<Animator>();
    }

    public void GetKnockedBack(Vector3 knockbackVector)
    {
        Debug.Log("GetKnockedBack called on PlayerCharacterController");
    }
}