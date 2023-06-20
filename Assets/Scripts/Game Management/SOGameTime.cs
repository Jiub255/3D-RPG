using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Time SO", fileName = "Game Time SO")]
public class SOGameTime : ScriptableObject, IResettable
{
    // 1 day = 30 minutes: 24 hours in a day, 2 half hours in an hour, 24 * 2 = 48;
	public float GameTimeMultiplier = 48f;

    // Start at noon, 43200 seconds = 12 hours. 
	private float _gameTime = 43200f;

    public void ResetOnExitPlayMode()
    {
        // 1 day = 30 minutes: 24 hours in a day, 2 half hours in an hour, 24 * 2 = 48;
        GameTimeMultiplier = 48;

        // Start at noon, 43200 seconds = 12 hours. 
        _gameTime = 43200f;
    }

    public void IncrementTime(float amount)
    {
        _gameTime += amount * GameTimeMultiplier;
    }

    public GameTime GetGameTime()
    {
        int totalhours = Mathf.FloorToInt(_gameTime / 3600f);
        int minutes = Mathf.FloorToInt(_gameTime / 60f) % 60;
        int seconds = Mathf.FloorToInt(_gameTime) % 60;
        int hours = totalhours % 24;
        bool am = true;
        if (hours >= 12)
        {
            hours %= 12;
            am = false;
        }
        if (hours == 0)
        {
            hours = 12;
        }
        return new GameTime(hours, minutes, seconds, am);
    }
}

public class GameTime
{
    public int Hours;
    public int Minutes;
    public int Seconds;
    public bool Am;

    public GameTime(int hours, int minutes,int seconds, bool am)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        Am = am;
    }
}