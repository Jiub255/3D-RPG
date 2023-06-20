using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Put this on Camera Focal Point
public class CameraMoveRotate : MonoBehaviour
{
    public static event Action<Vector3, Vector3> OnRotatedCamera;
    
    [SerializeField, Range(0f, 2f)]
    private float _rotationSpeed = 0.15f;
    [SerializeField, Range(0f, 40f)]
    private float _rotationXMin = 7f;
    [SerializeField, Range(50f, 90f)]
    private float _rotationXMax = 90f;
    [SerializeField, Range (0f, 1f)]
    private float _smoothTime = 0.3f;
    [SerializeField]
    private SOPlayerInstance _playerInstanceSO;
    [SerializeField]
    private SOVectors _vectorsSO;

    private Transform _transform;
    private Transform _playerTransform;
/*    private Vector3 _forward;
    private Vector3 _right;*/
    private InputAction _zoomAction;
    private InputAction _rotateCameraAction;
    private InputAction _mouseDeltaAction;
    private Vector3 _velocity = Vector3.zero;
    private bool _rotating = false;

    private void Start()
    { 
        _transform = transform;
        _playerTransform = _playerInstanceSO.PlayerInstanceTransform;

        _zoomAction = S.I.IM.PC.Camera.Zoom;
        _rotateCameraAction = S.I.IM.PC.Camera.RotateCamera;
        _mouseDeltaAction = S.I.IM.PC.Camera.MouseDelta;

        // Started toggles it to on, and then canceled toggles it back to off when you release the button. 
        S.I.IM.PC.Camera.RotateCamera.started += ToggleRotation;
        S.I.IM.PC.Camera.RotateCamera.canceled += ToggleRotation;

        GetVectors();
    }

    private void OnDisable()
    {
        S.I.IM.PC.Camera.RotateCamera.started -= ToggleRotation;
        S.I.IM.PC.Camera.RotateCamera.canceled -= ToggleRotation;
    }

    private void Update()
    {
        // Raise the position up to the player's head level. 
        Vector3 target = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1f, _playerTransform.position.z);

        _transform.position = Vector3.SmoothDamp(
            _transform.position,
            target,
            ref _velocity,
            _smoothTime,
            Mathf.Infinity,
            Time.unscaledDeltaTime);
    }

    private void ToggleRotation(InputAction.CallbackContext context)
    {
        _rotating = !_rotating;
    }

    private void LateUpdate()
    {
        // Zoom overrides everything else. Not noticeable since this action gets called only during
        // isolated frames, but it helps resolve some issues with moving while zooming.
        if (!_zoomAction.WasPerformedThisFrame())
        {
            if (_rotating)
            {
                GetVectors();
                RotateCamera();
            }
        }
    }

    // Only need to get these while rotating, because they don't change while moving or zooming. 
    // Also once when the script loads, so movement and zooming work from the beginning. 
    private void GetVectors()
    {
        Vector3 forward = _transform.forward;
        Vector3 right = _transform.right;

        // Project the forward and right vectors onto the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;

        // Normalize them
        forward.Normalize();
        right.Normalize();

        _vectorsSO.Forward = forward;
        _vectorsSO.Right = right;

        // Send the forward and right vectors to player movement script.
        // Is the right vector necessary? 
//        OnRotatedCamera?.Invoke(_forward, _right);
    }

    private void RotateCamera()
    {
        // Rotation around y-axis
        float deltaX =
            _mouseDeltaAction.ReadValue<Vector2>().x *
            _rotationSpeed;

        //Debug.Log($"Mouse delta: {_mouseDeltaAction.ReadValue<Vector2>()}");

        _transform.RotateAround(_transform.position, Vector3.up, deltaX);

        // Rotation around axis parallel to your local right vector, this axis always parallel to xz-plane.
        Vector3 axis = new Vector3(
            -Mathf.Cos(Mathf.Deg2Rad * _transform.rotation.eulerAngles.y),
            0f,
            Mathf.Sin(Mathf.Deg2Rad * _transform.rotation.eulerAngles.y));

        float deltaY =
            _mouseDeltaAction.ReadValue<Vector2>().y *
            _rotationSpeed;

        // Clamp x-rotation between min and max values (at most 0 - 90).
        if (_transform.rotation.eulerAngles.x - deltaY > _rotationXMin && _transform.rotation.eulerAngles.x - deltaY <= _rotationXMax)
        {
            _transform.RotateAround(_transform.position, axis, deltaY);
        }
    }
}