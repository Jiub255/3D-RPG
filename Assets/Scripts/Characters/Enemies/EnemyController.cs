using UnityEngine;
using UnityEngine.AI;

// Put "Any State" stuff in here?
public class EnemyController : StateRunner<EnemyController>, IKnockbackable
{
    public SOPlayerInstance PlayerInstanceSO;
    public Vector3 KnockbackVector { get; protected set; }
    [HideInInspector]
    public Animator Animator { get; protected set; }
    public NavMeshAgent NavMeshAgent { get; protected set; }


    protected override void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        EnemyHealthManager.OnEnemyDied += SetStatesToDead;

        base.Awake();
    }

    private void OnDisable()
    {
        EnemyHealthManager.OnEnemyDied -= SetStatesToDead;
    }

    private void SetStatesToDead()
    {
        Animator.SetTrigger("Dead");
        ChangeState(typeof(SOEnemyDeadState));
    }

    // Gets called from PlayerMeleeAttack on hit. Searches for IKnockbackable and calls this method. 
    // Want to be able to transition to knockback state from any state. This seems better
    // and cleaner than putting this in each individual state. 
    public void GetKnockedBack(Vector3 knockbackVector)
    {
        KnockbackVector = knockbackVector;
        ChangeState(typeof(SOEnemyKnockbackState));
        Debug.Log($"GetKnockedBack called on {gameObject.name}");
    }
}