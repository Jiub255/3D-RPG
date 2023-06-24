using UnityEngine;
using UnityEngine.AI;

// Put "Any State" stuff in here?
public class PlayerCharacterController2 : StateRunner2<PlayerCharacterController2>, IKnockbackable
{
    [SerializeField, Header("Movement State Variables")]
    protected float _speed = 10f;
    [SerializeField]
    protected float _turnSpeed = 15f;
    public SOVectors VectorsSO;

    [SerializeField, Header("Attack State Variables")]
    protected AnimationClip _attackAnimationClip;

    [SerializeField, Header("Knockback State Variables")]
    protected float _knockbackDuration = 0.5f;

    public Animator Animator { get; protected set; }
    public NavMeshAgent NavMeshAgent { get; protected set; }
    public Vector3 KnockbackVector { get; protected set; }

    protected override void Awake()
    {
        // Need to get references before calling first state's constructor? 
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // Start in movement state. 
        _activeState = Movement();
        
//        base.Awake();
    }

    public void GetKnockedBack(Vector3 knockbackVector)
    {
        KnockbackVector = knockbackVector;
        ChangeState2(Knockback());
        Debug.Log("GetKnockedBack called on PlayerCharacterController");
    }

    public State<PlayerCharacterController2> Movement() { return new PlayerMovementState(this, _speed, _turnSpeed); }
    public State<PlayerCharacterController2> Attack() { return new PlayerAttackState(this, _attackAnimationClip); }
    public State<PlayerCharacterController2> Dialog() { return new PlayerDialogState(this); }
    public State<PlayerCharacterController2> Knockback() { return new PlayerKnockbackState(this, _knockbackDuration); }
}