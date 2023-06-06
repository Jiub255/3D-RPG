using UnityEngine;

[CreateAssetMenu(menuName = "Player/Player Health", fileName = "Player Health")]
public class SOPlayerHealth : ScriptableObject , IResettable
{
	public int MaxHealth = 3;
	public int CurrentHealth = 3;

    public void ResetOnExitPlayMode()
    {
        CurrentHealth = MaxHealth;
    }
}