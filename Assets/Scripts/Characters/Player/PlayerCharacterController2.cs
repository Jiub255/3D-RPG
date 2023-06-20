using UnityEngine;
using UnityEngine.AI;

// Put "Any State" stuff in here?
public class PlayerCharacterController2 : StateRunner2<PlayerCharacterController2>
{
    // Movement state variables
    [SerializeField, Header("Movement State Variables")]
    protected float _speed = 10f;
    [SerializeField]
    protected float _turnSpeed = 15f;
    public SOVectors VectorsSO;

    // Attack state variables
    [SerializeField, Header("Attack State Variables")]
    protected AnimationClip _attackAnimationClip;

    public Animator Animator { get; protected set; }
    public NavMeshAgent NavMeshAgent { get; protected set; }

    protected override void Awake()
    {
        // Need to get references before calling first state's constructor? 
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        // Start in movement state. 
        _activeState = Movement();
        
//        base.Awake();
    }   

    public State<PlayerCharacterController2> Movement()
    {
        return new PlayerMovementState(this, _speed, _turnSpeed);
    }
    public State<PlayerCharacterController2> Attack()
    {
        return new PlayerAttackState(this, _attackAnimationClip);
    }
    public State<PlayerCharacterController2> Dialog()
    {
        return new PlayerDialogState(this);
    }
}