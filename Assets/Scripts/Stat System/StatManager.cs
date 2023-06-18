using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
	public static event Action OnStatsChanged;

	[SerializeField]
	private SOStats _statsSO;
	[SerializeField]
	private SOEquipment _equipmentSO;

    private void Start()
    {
		CalculateStatModifiers();
    }

    public SOStat NameToSOStat(string name)
    {
        foreach (SOStat stat in _statsSO.StatSOs)
        {
            if (stat.name == name)
            {
                return stat;
            }
        }

        Debug.LogWarning($"No stat found with the name {name}. ");
        return null;
    }

    private void OnEnable()
    {
        _equipmentSO.OnEquipmentChanged += CalculateStatModifiers;
        SOStat.OnStatChanged += CalculateStatModifiers;
    }

    private void OnDisable()
    {
        _equipmentSO.OnEquipmentChanged -= CalculateStatModifiers;
        SOStat.OnStatChanged -= CalculateStatModifiers;
    }

    private void CalculateStatModifiers()
    {
        // Clear all modifiers on all stats. 
        foreach (SOStat stat in _statsSO.StatSOs)
        {
            stat.ClearModifiers();
        }

        // Add each bonus.
        foreach (SOEquipmentItem equipmentItem in _equipmentSO.EquipmentItems)
        {
            foreach (EquipmentBonus bonus in equipmentItem.Bonuses)
            {
                bonus.StatSO.AddModifier(bonus.BonusAmount);
            }
        }
    
        // Heard by UIStats and PlayerMeleeAttack. 
        OnStatsChanged?.Invoke();
    }
}