using UnityEngine;

// Attached to the weapon child of the model's hand. 
public class PlayerMeleeWeapon : MonoBehaviour
{
    [SerializeField]
    private StatManager _statManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(_statManager.NameToSOStat("Attack").GetValue());
        }
    }
}