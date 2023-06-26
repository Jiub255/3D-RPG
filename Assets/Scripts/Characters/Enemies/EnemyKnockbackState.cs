using UnityEngine;

public class EnemyKnockbackState : State<EnemyController2>
{
    // TODO - Make this the length of the get hit animation?
    // Could speed up or slow down the animation itself to feel right. 
    // At least make it as long as the animation, so it has time to run through,
    // no big deal if the end of the knockback is in idle animation, might even look realistic. 
    protected float _knockbackDuration;
    protected Vector3 _knockbackVector;
    protected float _timer;
//    protected Rigidbody _rigidbody;\

    protected float _speed;
    protected float _angularSpeed;
    protected float _acceleration;

    public EnemyKnockbackState(EnemyController2 parent, float knockbackDuration, Vector3 knockbackVector) : base(parent)
    {
        _knockbackDuration = knockbackDuration;
        _knockbackVector = knockbackVector;

        _timer = 0f;

        // Use _runner.NavMeshAgent to do knockback. Set velocity in knockback direction and disable rotation and/or other properties. 
        // Save original values of NavMeshAgent. 
        _speed = parent.NavMeshAgent.speed;
        _angularSpeed = parent.NavMeshAgent.angularSpeed;
        _acceleration = parent.NavMeshAgent.acceleration;

        // Set new values for knockback. 
        parent.NavMeshAgent.speed = 10;
        // Keeps the enemy facing forwad instead of spinning. 
        parent.NavMeshAgent.angularSpeed = 0;
        parent.NavMeshAgent.acceleration = 20;

        // Animation
        parent.Animator.SetTrigger("GetHit");
    }

    public override void Update() 
    {
        _timer += Time.deltaTime;
        if (_timer > _knockbackDuration)
        {
            _runner.ChangeStateTo(_runner.Idle());
        }
    }

    public override void FixedUpdate()
    {
        _runner.NavMeshAgent.velocity = _knockbackVector;
    }

    public override void Exit() 
    {
        // Set back to default settings. 
        _runner.NavMeshAgent.speed = _speed;
        _runner.NavMeshAgent.angularSpeed = _angularSpeed;
        _runner.NavMeshAgent.acceleration = _acceleration;

        // Set velocity to zero to lessen sliding. 
        _runner.NavMeshAgent.velocity = Vector3.zero;
    }

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