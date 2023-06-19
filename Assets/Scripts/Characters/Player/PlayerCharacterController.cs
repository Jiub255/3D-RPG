using UnityEngine;

// Put "Any State" stuff in here?
public class PlayerCharacterController : StateRunner<PlayerCharacterController>, IKnockbackable
{
    public Animator Animator { get; protected set; }
    public Vector3 KnockbackVector { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }

    protected override void Awake()
    {
        // Need to get references before calling base.Awake(). 
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

        base.Awake();
    }   

    public void GetKnockedBack(Vector3 knockbackVector)
    {
        KnockbackVector = knockbackVector;
        ChangeState(typeof(SOPlayerKnockbackState));
        Debug.Log("GetKnockedBack called on PlayerCharacterController");
    }

/*    public void ACoupleOfStupidFixesThatIHate()
    {
        // Stupid fixes, need to do this before disabling action maps so that an
        // in progress action gets "completed" and not stuck in progress
        // or whatever was happening. 
        // This fixes the bug but causes errors, other action maps wont work after.  
        S.I.IM.PC.Movement.MovePlayer.Dispose();
        S.I.IM.PC.Movement.Melee.Dispose();
        S.I.IM.PC.Movement.Interact.Dispose();
        // This fixes the bug introduced by the above bug fix. I don't like it. Maybe try upgrading unity/input system instead? 
        S.I.IM.PC.Enable();
    }*/
}