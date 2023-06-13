using UnityEngine;

public class TESTenemyHealthManager : MonoBehaviour, IDamageable
{
	[SerializeField]
	private int _maxHealth = 1;

	private int _health;

    public void Die()
    {
        Debug.Log($"{transform.root.name} died. ");
        transform.root.gameObject.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    private void Awake()
    {
        _health = _maxHealth;
    }
}