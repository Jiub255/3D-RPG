using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private SOStat _attackStatSO;
    [SerializeField]
    private SOStat _knockbackStatSO;
    [SerializeField]
    private Transform _attackBox;
    [SerializeField]
    private LayerMask _enemyLayerMask;

    private int _attack;
    private int _knockback;

    private void OnEnable()
    {
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

    public void CheckForHits()
    {
        Collider[] hits = Physics.OverlapBox(_attackBox.position, _attackBox.localScale / 2, Quaternion.identity, _enemyLayerMask);
        foreach (Collider hit in hits)
        {
            Debug.Log($"Hit {hit.name}");

            // Attack damage. 
            if (hit.GetComponent<IDamageable>() != null)
            {
                hit.GetComponent<IDamageable>().TakeDamage(_attack);
            }

            // Knockback
            if (hit.GetComponent<IKnockbackable>() != null)
            {
                Vector3 direction = hit.transform.position - transform.root.position;
                Vector3 xzProjection = new Vector3(direction.x, 0f, direction.z);
                Vector3 normalized = xzProjection.normalized;
                Vector3 knockbackVector = normalized * _knockback;
                Debug.Log($"Knockback vector: {knockbackVector}");
                hit.GetComponent<IKnockbackable>().GetKnockedBack(knockbackVector);
            }
        }
    }
}