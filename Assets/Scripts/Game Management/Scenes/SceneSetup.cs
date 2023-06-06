using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField]
    private SOPlayerInstance _playerInstanceSO;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Transform _startingPosition;

    [SerializeField]
    private GameObject _uiPrefab;

    [SerializeField]
    private GameObject _camerasPrefab;

/*    [SerializeField]
    private AudioClip _sceneMusic;*/

    private void Awake()
    {
        // Instantiate player at starting position, and set the instance's transform in _playerInstanceSO. 
        _playerInstanceSO.PlayerInstanceTransform = Instantiate(_playerPrefab, _startingPosition.position, Quaternion.identity).transform;

        // Instantiate cameras object. 
        Instantiate(_camerasPrefab);
        
        // Instantiate menus. 
        Instantiate(_uiPrefab);

        // Start playing the scene music. 
        //S.I.AudioManager.PlayMusic(_sceneMusic);
    }
}