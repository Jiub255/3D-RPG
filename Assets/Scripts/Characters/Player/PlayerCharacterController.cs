using UnityEngine;
using UnityEngine.AI;

// Put "Any State" stuff in here?
public class PlayerCharacterController : StateRunner<PlayerCharacterController>, IKnockbackable
{
    public Animator Animator { get; protected set; }
    public Vector3 KnockbackVector { get; protected set; }
//    public Rigidbody Rigidbody { get; protected set; }
    public NavMeshAgent NavMeshAgent { get; protected set; }

    protected override void Awake()
    {
        // Need to get references before calling base.Awake(). 
        Animator = GetComponentInChildren<Animator>();
//        Rigidbody = GetComponent<Rigidbody>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        base.Awake();
    }   

    public void GetKnockedBack(Vector3 knockbackVector)
    {
        KnockbackVector = knockbackVector;
        ChangeState(typeof(SOPlayerKnockbackState));
        Debug.Log("GetKnockedBack called on PlayerCharacterController");
    }
}