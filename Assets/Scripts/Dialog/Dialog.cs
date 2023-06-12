using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public void Setup(string dialogText)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = dialogText;
    }
}