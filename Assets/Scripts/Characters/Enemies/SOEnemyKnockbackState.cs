using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Knockback State", fileName = "Enemy Knockback State")]
public class SOEnemyKnockbackState : SOState<EnemyController>
{
    // TODO - Make this the length of the get hit animation?
    // Could speed up or slow down the animation itself to feel right. 
    // At least make it as long as the animation, so it has time to run through,
    // no big deal if the end of the knockback is in idle animation, might even look realistic. 
    [SerializeField]
    protected float _knockbackDuration = 0.5f;
    protected float _timer;
//    protected Rigidbody _rigidbody;\

    protected float _speed;
    protected float _angularSpeed;
    protected float _acceleration;

    public override void Init(EnemyController parent)
    {
        base.Init(parent);

        _timer = 0f;

//        EnemyHealthManager.OnEnemyDied += () => { _runner.Animator.SetBool("Dead", true); };

//        _rigidbody = _runner.GetComponent<Rigidbody>();
//        _rigidbody.AddForce(_runner.KnockbackVector, ForceMode.Impulse);

        // TODO - Use _runner.NavMeshAgent to do knockback. Set velocity in knockback direction and disable rotation and/or other properties. 
        // Save original values of NavMeshAgent. 
        _speed = _runner.NavMeshAgent.speed;
        _angularSpeed = _runner.NavMeshAgent.angularSpeed;
        _acceleration = _runner.NavMeshAgent.acceleration;

        // Set new values for knockback. 
        _runner.NavMeshAgent.speed = 10;
        // Keeps the enemy facing forwad instead of spinning. 
        _runner.NavMeshAgent.angularSpeed = 0;
        _runner.NavMeshAgent.acceleration = 20;

        // Animation
        _runner.Animator.SetTrigger("GetHit");
    }

    public override void CheckForStateChangeConditions()
    {
        _timer += Time.deltaTime;
        // Why is it transitioning to dead state after one hit, even though the animator bool stays false? 
        if (_timer > _knockbackDuration)
        {
           // if (!_runner.Animator.GetBool("Dead"))
            {
                _runner.ChangeState(typeof(SOEnemyIdleState));
            }
/*            else
            {
                _runner.ChangeState(typeof(SOEnemyDeadState));
            }*/
        }
    }

    public override void FixedUpdate()
    {
        _runner.NavMeshAgent.velocity = _runner.KnockbackVector;
    }

    public override void Exit() 
    {
        // Set back to default settings. 
        _runner.NavMeshAgent.speed = 10;
        _runner.NavMeshAgent.angularSpeed = 0;//Keeps the enemy facing forwad rther than spinning
        _runner.NavMeshAgent.acceleration = 20;

        // Set velocity to zero to lessen sliding. 
        // Set velocity to zero if enemy died. 
//        if (_runner.Animator.GetBool("Dead"))
        {
            _runner.NavMeshAgent.velocity = Vector3.zero;
        }

//         EnemyHealthManager.OnEnemyDied -= () => { _runner.Animator.SetBool("Dead", true); };
    }

    public override void CaptureInput() {}
    public override void Update() {}

    // Attempt at a smoother knockback. Might try again later. 
/*        private IEnumerator KnockbackCoroutine(Vector3 knockbackVector)
    {
        float timer = 0f;
        while (timer < _knockbackDuration)
        {
            float lerpMult = Mathf.Lerp(1, 0, timer / _knockbackDuration);
            Debug.Log($"Timer: {timer}, LerpMult: {lerpMult}"); 
            _rb.AddForce(knockbackVector * _knockbackMult * lerpMult, ForceMode.Force);
            timer += Time.deltaTime;
            yield return null;
        }
    }*/
}