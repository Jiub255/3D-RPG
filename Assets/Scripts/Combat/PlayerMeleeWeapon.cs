using UnityEngine;

// Attached to the weapon child of the model's hand. 
public class PlayerMeleeWeapon : MonoBehaviour
{
    [SerializeField]
    private SOStat _attackStatSO;
    [SerializeField]
    private SOStat _knockbackStatSO;

    private int _attack;
    private int _knockback;
    private Collider _weaponCollider;

    private void OnEnable()
    {
        _weaponCollider = GetComponent<Collider>();
        DisableCollider();
        CalculateAttack();
        CalculateKnockback();

        StatManager.OnStatsChanged += CalculateAttack;
        StatManager.OnStatsChanged += CalculateKnockback;
    }

    private void OnDisable()
    {
        StatManager.OnStatsChanged -= CalculateAttack;
        StatManager.OnStatsChanged -= CalculateKnockback;
    }

    // Called by PlayerMelee when starting an attack. 
    public void EnableCollider()
    {
        _weaponCollider.enabled = true;
    }

    // Called by animation event at the end of the attack animation. 
    public void DisableCollider()
    {
        _weaponCollider.enabled = false;
    }

    private void CalculateAttack()
    {
        if (_attackStatSO != null)
        {
            _attack = _attackStatSO.GetValue();
        }
        else
        {
            Debug.LogWarning("No Attack stat found. ");
        }
    }

    private void CalculateKnockback()
    {
        if (_knockbackStatSO != null)
        {
            _knockback = _knockbackStatSO.GetValue();
        }
        else
        {
            Debug.LogWarning("No Knockback stat found. ");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(_attack);
        }

        // Just for testing. 
        if (other.GetComponent<TESTEnemyHitAnim>() != null)
        {
            other.GetComponent<TESTEnemyHitAnim>().GetHit();
        }

        // Knockback
        if (other.GetComponent<TESTKnockback>() != null)
        {
            Vector3 direction = other.transform.position - transform.root.position;
            Vector3 xzProjection = new Vector3(direction.x, 0f, direction.z);
            Vector3 normalized = xzProjection.normalized;
            Vector3 knockbackVector = normalized * _knockback;
            Debug.Log($"Knockback vector: {knockbackVector}");
            other.GetComponent<TESTKnockback>().GetKnockedBack(knockbackVector);
        }
    }
}