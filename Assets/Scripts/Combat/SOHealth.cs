using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Health SO", fileName = "Health SO")]
public class SOHealth : ScriptableObject , IResettable
{
	public int MaxHealth = 3;
	public int CurrentHealth = 3;

    public void ResetOnExitPlayMode()
    {
        CurrentHealth = MaxHealth;
    }
}