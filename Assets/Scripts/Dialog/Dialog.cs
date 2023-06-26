using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    protected TextMeshProUGUI _dialogText;

    private void OnEnable()
    {
        _dialogText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(string dialogText)
    {
        _dialogText.text = dialogText;
    }
}