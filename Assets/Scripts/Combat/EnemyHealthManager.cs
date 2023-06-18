using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamageable
{
	[SerializeField]
	private SOHealth _healthSO;

    public void Die()
    {
        Debug.Log($"{transform.gameObject.name} died. ");
        transform.gameObject.SetActive(false);
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

    private void Awake()
    {
        _healthSO.CurrentHealth = _healthSO.MaxHealth;
    }
}