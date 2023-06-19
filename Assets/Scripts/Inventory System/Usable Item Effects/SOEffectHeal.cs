using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Heal effect", fileName = "Heal Effect")]
public class SOEffectHeal : SOEffect
{
    public static event Action<int> OnHealEffect;

    public int HealAmount;

    public override void ExecuteEffect(SOUsableItem item)
    {
        Debug.Log("Heal effect called");

        // PlayerHealthManager listens. 
        OnHealEffect?.Invoke(HealAmount);
    }
}