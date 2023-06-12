using TMPro;
using UnityEngine;

public class Response : MonoBehaviour
{
    private int _choiceIndex;

    public void Setup(string dialogText, int choiceIndex)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = dialogText;
        _choiceIndex = choiceIndex;
    }
}