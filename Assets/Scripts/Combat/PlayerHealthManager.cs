using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IHealable, IDamageable
{
	[SerializeField]
	private SOHealth _playerHealthSO;

    private void OnEnable()
    {
        SOEffectHeal.OnHealEffect += Heal;
    }

    private void OnDisable()
    {
        SOEffectHeal.OnHealEffect -= Heal;
    }

    public void Heal(int amount)
    {
        _playerHealthSO.CurrentHealth += amount;
        if (_playerHealthSO.CurrentHealth > _playerHealthSO.MaxHealth)
        {
            _playerHealthSO.CurrentHealth = _playerHealthSO.MaxHealth;
        }
    }

    public void FullHeal()
    {
        _playerHealthSO.CurrentHealth = _playerHealthSO.MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        _playerHealthSO.CurrentHealth -= amount;
        if (_playerHealthSO.CurrentHealth <= 0)
        {
            _playerHealthSO.CurrentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("You Died");

        // Send player death event. 
    }
}