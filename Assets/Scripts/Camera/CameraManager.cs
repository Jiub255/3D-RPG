using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[SerializeField] 
	private Camera _gameplayCamera;
	[SerializeField] 
	private Camera _dialogCamera;
    [SerializeField]
    private float _heightOffset = 1f;

    private void OnEnable()
    {
        NPCDialog.OnInteractWithNPC += SwitchToDialogCamera;
        UIDialog.OnDialogEnd += SwitchToGameplayCamera;
    }

    private void OnDisable()
    {
        NPCDialog.OnInteractWithNPC -= SwitchToDialogCamera;
        UIDialog.OnDialogEnd -= SwitchToGameplayCamera;
    }

    private void SwitchToDialogCamera(Transform npcTransform)
    {
        _gameplayCamera.enabled = false;
        _dialogCamera.enabled = true;
    
        _dialogCamera.transform.LookAt(new Vector3(
            npcTransform.position.x, 
            npcTransform.position.y + _heightOffset,
            npcTransform.position.z));
    }

    private void SwitchToGameplayCamera()
    {
        _gameplayCamera.enabled = true;
        _dialogCamera.enabled = false;
    }
}