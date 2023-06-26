using UnityEngine;
using UnityEngine.AI;
// Put "Any State" stuff in here?
public class EnemyController2 : StateRunner2<EnemyController2>, IKnockbackable
{
    [SerializeField, Header("Idle State Variables")]
    private float _chaseRadius;

    [SerializeField, Header("Approach Player State Variables")]
    protected float _attackRadius = 2f;

    [SerializeField, Header("Attack State Variables")]
    protected AnimationClip _attackAnimationClip;

    [SerializeField, Header("Knockback State Variables")]
    protected float _knockbackDuration = 0.5f;
    protected Vector3 _knockbackVector;

    // Used by multiple states. 
    [Header("Multi-State Variables")]
    public SOPlayerInstance PlayerInstanceSO;
    [HideInInspector]
    public Animator Animator { get; protected set; }
    [HideInInspector]
    public NavMeshAgent NavMeshAgent { get; protected set; }

    protected override void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        EnemyHealthManager.OnEnemyDied += HandleEnemyDeath;

        // Start in Idle state.
        _activeState = Idle();
    }

    private void OnDisable()
    {
        EnemyHealthManager.OnEnemyDied -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        Animator.SetTrigger("Dead");

        ChangeStateTo(Dead());

        GetComponent<Collider>().enabled = false;

        NavMeshAgent.enabled = false;
    }

    // Gets called from PlayerMeleeAttack on hit. Searches for IKnockbackable and calls this method. 
    // Want to be able to transition to knockback state from any state. This seems better
    // and cleaner than putting this in each individual state. 
    public void GetKnockedBack(Vector3 knockbackVector)
    {
        _knockbackVector = knockbackVector;
        ChangeStateTo(Knockback());
        //        Debug.Log($"GetKnockedBack called on {gameObject.name}");
    }

    // These methods create, initialize, and set _activeState to, a new state everytime the state is changed. 
    public State<EnemyController2> Idle() { return new EnemyIdleState(this, _chaseRadius); }
    public State<EnemyController2> ApproachPlayer() { return new EnemyApproachPlayerState(this, _attackRadius); }
    public State<EnemyController2> Attack() { return new EnemyAttackState(this, _attackAnimationClip); }
    public State<EnemyController2> Knockback() { return new EnemyKnockbackState(this, _knockbackDuration, _knockbackVector); }
    public State<EnemyController2> Dead() { return new EnemyDeadState(this); }
}