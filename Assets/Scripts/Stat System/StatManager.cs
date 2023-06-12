using System;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
	public static event Action OnStatsChanged;

	[SerializeField]
	private List<SOStat> _statSOs;
	[SerializeField]
	private SOEquipment _equipmentSO;

    private void Start()
    {
		CalculateStatModifiers();
    }

    public SOStat NameToSOStat(string name)
    {
        foreach (SOStat stat in _statSOs)
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
    }

    private void OnDisable()
    {
        _equipmentSO.OnEquipmentChanged -= CalculateStatModifiers;
    }

    private void CalculateStatModifiers()
    {
        // Clear all modifiers on all stats. 
        foreach (SOStat stat in _statSOs)
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
    
        // Heard by UIStats. 
       // OnStatsChanged?.Invoke();
    }
}