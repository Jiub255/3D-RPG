using TMPro;
using UnityEngine;

public class UIGameTime : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _hoursText;
    [SerializeField]
    private TextMeshProUGUI _minutesText;
    [SerializeField]
    private TextMeshProUGUI _secondsText;
    [SerializeField]
    private TextMeshProUGUI _amPmText;

    private void FixedUpdate()
    {
        GameTime gameTime = S.I.GameManager.GetGameTime();

        _hoursText.text = gameTime.Hours.ToString();
        _minutesText.text = gameTime.Minutes.ToString("00");
        _secondsText.text = gameTime.Seconds.ToString("00");
        _amPmText.text = gameTime.Am ? "AM" : "PM";
    }
}