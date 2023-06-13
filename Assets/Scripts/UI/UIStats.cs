using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStats : MonoBehaviour
{
	[SerializeField]
	private SOStats _statsSO;
	[SerializeField]
	private GameObject _statPanelPrefab;
	[SerializeField]
	private Transform _scrollViewContent;

    private void OnEnable()
    {
		SetupInventorySlots();

		MenuController.OnOpenStatsMenu += SetupInventorySlots;
    }

    private void OnDisable()
    {
        MenuController.OnOpenStatsMenu -= SetupInventorySlots;
    }

    private void SetupInventorySlots()
	{
		ClearPanels();

		foreach (SOStat statSO in _statsSO.StatSOs)
        {
			GameObject panel = Instantiate(_statPanelPrefab, _scrollViewContent);
			panel.GetComponent<StatPanel>().SetupStatPanel(statSO);
        }
	}

	private void ClearPanels()
	{
		foreach (Transform panelTransform in _scrollViewContent)
		{
			Destroy(panelTransform.gameObject);
		}
	}
}