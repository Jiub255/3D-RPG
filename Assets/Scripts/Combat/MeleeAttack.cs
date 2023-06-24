using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    protected SOStat _attackStatSO;
    [SerializeField]
    protected SOStat _knockbackStatSO;
    [SerializeField]
    protected Transform _attackBox;
    [SerializeField]
    protected LayerMask _targetLayerMask;

    protected int _attack;
    protected int _knockbackForce;

    protected virtual void OnEnable()
    {
        CalculateAttack();
        CalculateKnockback();
    }

    protected virtual void CalculateAttack()
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

    protected virtual void CalculateKnockback()
    {
        if (_knockbackStatSO != null)
        {
            _knockbackForce = _knockbackStatSO.GetValue();
        }
        else
        {
            Debug.LogWarning("No Knockback stat found. ");
        }
    }

    public virtual void CheckForHits()
    {
        Collider[] hits = Physics.OverlapBox(_attackBox.position, _attackBox.localScale / 2, Quaternion.identity, _targetLayerMask);
        foreach (Collider hit in hits)
        {
//            Debug.Log($"Hit {hit.name}");

            // Knockback
            // Do knockback before damage so knockback state can be entered before dying. 
            if (hit.GetComponent<IKnockbackable>() != null)
            {
                Vector3 direction = hit.transform.position - transform.parent.position;
                Vector3 xzProjection = new Vector3(direction.x, 0f, direction.z);
                Vector3 normalized = xzProjection.normalized;
                Vector3 knockbackVector = normalized * _knockbackForce;
//                Debug.Log($"Knockback vector: {knockbackVector}");
                hit.GetComponent<IKnockbackable>().GetKnockedBack(knockbackVector);
            }

            // Attack damage. 
            if (hit.GetComponent<IDamageable>() != null)
            {
                hit.GetComponent<IDamageable>().TakeDamage(_attack);
            }
        }
    }
}