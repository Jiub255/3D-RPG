using UnityEngine;
using UnityEngine.AI;

// Put "Any State" stuff in here?
public class PlayerCharacterController2 : StateRunner2<PlayerCharacterController2>, IKnockbackable
{
    [SerializeField, Header("Movement State Variables")]
    protected float _speed = 10f;
    [SerializeField]
    protected float _turnSpeed = 15f;
    [SerializeField]
    private SOVectors _vectorsSO;

    [SerializeField, Header("Attack State Variables")]
    protected AnimationClip _attackAnimationClip;

    [SerializeField, Header("Knockback State Variables")]
    protected float _knockbackDuration = 0.5f;
    protected Vector3 _knockbackVector;

    // Used by multiple states. 
    public Animator Animator { get; protected set; }
    public NavMeshAgent NavMeshAgent { get; protected set; }

    protected override void Awake()
    {
        // Need to get references before calling first state's constructor? 
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // Start in movement state. 
        _activeState = Movement();
    }

    public void GetKnockedBack(Vector3 knockbackVector)
    {
        _knockbackVector = knockbackVector;
        ChangeStateTo(Knockback());
        Debug.Log("GetKnockedBack called on PlayerCharacterController");
    }

    // These methods create, initialize, and set _activeState to, a new state everytime the state is changed. 
    public State<PlayerCharacterController2> Movement() { return new PlayerMovementState(this, _speed, _turnSpeed, _vectorsSO); }
    public State<PlayerCharacterController2> Attack() { return new PlayerAttackState(this, _attackAnimationClip); }
    public State<PlayerCharacterController2> Dialog() { return new PlayerDialogState(this); }
    public State<PlayerCharacterController2> Knockback() { return new PlayerKnockbackState(this, _knockbackDuration, _knockbackVector); }
}