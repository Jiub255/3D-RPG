using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField]
	private SOPlayerHealth _playerHealthSO;

    private void OnEnable()
    {
        SOEffectHeal.OnHealEffect += Heal;
    }

    private void OnDisable()
    {
        SOEffectHeal.OnHealEffect -= Heal;
    }

    private void LoseHealth(int amount)
    {
        _playerHealthSO.CurrentHealth -= amount;
        if (_playerHealthSO.CurrentHealth <= 0)
        {
            _playerHealthSO.CurrentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Died");

        // Send player death event. 
    }

    private void Heal(int amount)
    {
        _playerHealthSO.CurrentHealth += amount;
        if (_playerHealthSO.CurrentHealth > _playerHealthSO.MaxHealth)
        {
            _playerHealthSO.CurrentHealth = _playerHealthSO.MaxHealth;
        }
    }
}