using System;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamageable
{
    public static event Action OnEnemyDied;

	[SerializeField]
	protected SOHealth _healthSO;

    protected virtual void Awake()
    {
        _healthSO.CurrentHealth = _healthSO.MaxHealth;
    }

    public void Die()
    {
        Debug.Log($"{transform.gameObject.name} died. ");
       // transform.gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;

        // Who listens?
        // SOEnemyKnockbackState listens, sets dying bool to true, 
        // so enemy can go to dead state after being knocked back. 
        OnEnemyDied?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        _healthSO.CurrentHealth -= amount;
        if (_healthSO.CurrentHealth <= 0)
        {
            _healthSO.CurrentHealth = 0;
            Die();
        }
    }
}