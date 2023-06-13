using TMPro;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _nameText;
	[SerializeField]
	private TextMeshProUGUI _valueText;

	public void SetupStatPanel(SOStat statSO)
    {
		_nameText.text = statSO.name;
		_valueText.text = statSO.GetValue().ToString();
    }
}