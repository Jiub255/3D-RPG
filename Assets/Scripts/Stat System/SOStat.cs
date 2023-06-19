using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Stat", fileName = "New Stat SO")]
public class SOStat : ScriptableObject
{
    public static event System.Action OnStatChanged;

    [SerializeField]
    protected int _baseValue;

    // Only serialized to see in the inspector. 
# if UNITY_EDITOR
    [SerializeField]
# endif
    protected List<int> _modifiers = new();

    // Just to see in the inspector, variable has no real use. 
    // TODO - Delete before building, waste of resources. 
    [SerializeField]
    protected int _moddedValue;

    public int GetValue()
    {
        int finalValue = _baseValue;
        _modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void ChangeBaseValue(int amount)
    {
        _baseValue += amount;
        _moddedValue = GetValue();
        // StatManager listens. 
        OnStatChanged?.Invoke();
    }

    public void AddModifier(int modifier)
    {
        _modifiers.Add(modifier);

        _moddedValue = GetValue();
    }

    public void RemoveModifier(int modifier)
    {
        _modifiers.Remove(modifier);

        _moddedValue = GetValue();
    }

    public void ClearModifiers()
    {
        _modifiers.Clear();

        _moddedValue = GetValue();
    }
}