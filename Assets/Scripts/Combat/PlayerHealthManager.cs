using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IHealable, IDamageable
{
	[SerializeField]
	private SOHealth _playerHealthSO;
    [SerializeField]
    private SOStat _maxHealthStatSO;

    private void OnEnable()
    {
        // Set max health based on whichever stats. 
        CalculateMaxHealth();

        SOEffectHeal.OnHealEffect += Heal;
        StatManager.OnStatsChanged += CalculateMaxHealth;
    }

    private void OnDisable()
    {
        SOEffectHeal.OnHealEffect -= Heal;
        StatManager.OnStatsChanged -= CalculateMaxHealth;
    }

    private void CalculateMaxHealth()
    {
        _playerHealthSO.MaxHealth = _maxHealthStatSO.GetValue();
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
        Debug.Log("FullHeal called in PlayerHealthManager");
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