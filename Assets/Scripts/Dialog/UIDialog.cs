using Ink.Runtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
    // Heard by MenuController. 
    public static event System.Action OnDialogEnd;

    [SerializeField] 
    private SOTextAsset _textAssetSO;

    [SerializeField]
    private GameObject _dialogPrefab;
    [SerializeField] 
    private GameObject _responsePrefab;

    [SerializeField]
    private GameObject _dialogContent;
    [SerializeField] 
    private GameObject _responseContent;
    [SerializeField] 
    private ScrollRect _dialogScrollRect;

    // TODO - Make this a list of Storys so the character can change stories depending on whatever factors. 
    private Story _story;

    private void OnEnable()
    {
        StartDialog();
    }

    private void StartDialog()
    {
        SetStory();
        RefreshView();
    }

    private void SetStory()
    {
        if (_textAssetSO.TextAsset)
        {
            _story = new Story(_textAssetSO.TextAsset.text);
        }
        else
        {
            Debug.Log("_dialogueValue.TextAsset == null");
        }
    }

    private void RefreshView()
    {
        while (_story.canContinue)
        {
            MakeNewDialog(_story.Continue());
        }

        if (_story.currentChoices.Count > 0)
        {
            MakeNewChoices();
            StartCoroutine(ResetScrollBar());
        }
        else
        {
            StartCoroutine(ResetScrollBar());
            EndDialog();
        }
    }

    private void EndDialog()
    {
        OnDialogEnd?.Invoke();
    }

    private IEnumerator ResetScrollBar()
    {
        yield return null;
        _dialogScrollRect.verticalNormalizedPosition = 0f;
    }

    private void MakeNewDialog(string dialogText)
    {
        Dialog newDialog = Instantiate(_dialogPrefab,
            _dialogContent.transform).GetComponent<Dialog>();

        newDialog.Setup(dialogText);
    }

    private void MakeNewResponse(string newDialogText, int choiceIndex)
    {
        Response newResponse = Instantiate(_responsePrefab,
            _responseContent.transform).GetComponent<Response>();

        newResponse.Setup(newDialogText, choiceIndex);

        Button responseButton = newResponse.gameObject.GetComponent<Button>();

        if (responseButton)
        {
            responseButton.onClick.AddListener(delegate { Choose(choiceIndex); });
        }
    }

    private void MakeNewChoices()
    {
        foreach (Transform childTransform in _responseContent.transform)
        {
            Destroy(childTransform.gameObject);
        }

        for (int i = 0; i < _story.currentChoices.Count; i++)
        {
            MakeNewResponse(_story.currentChoices[i].text, i);
        }
    }

    private void Choose(int choice)
    {
        _story.ChooseChoiceIndex(choice);

        RefreshView();
    }
}