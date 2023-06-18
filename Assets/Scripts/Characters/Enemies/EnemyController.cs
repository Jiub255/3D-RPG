using UnityEngine;

// Put "Any State" stuff in here?
public class EnemyController : StateRunner<EnemyController>, IKnockbackable
{
    public SOPlayerInstance PlayerInstanceSO;
    protected Vector3 _knockbackVector;
    [HideInInspector]
    public Animator Animator;

    protected override void Awake()
    {
        base.Awake();

        Animator = GetComponentInChildren<Animator>();
    }

    // Gets called from PlayerMeleeAttack on hit. Searches for IKnockbackable and calls this method. 
    // Want to be able to transition to knockback state from any state. This seems better
    // and cleaner than putting this in each individual state. 
    public void GetKnockedBack(Vector3 knockbackVector)
    {
        _knockbackVector = knockbackVector;
        ChangeState(typeof(SOEnemyKnockbackState));
    }
}