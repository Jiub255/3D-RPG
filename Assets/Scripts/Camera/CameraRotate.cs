using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Put this on Camera Focal Point
public class CameraRotate : MonoBehaviour
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
    private Transform _playerTransform;

    private Transform _transform;
    private Vector3 _forward;
    private Vector3 _right;
    private InputAction _zoomAction;
    private InputAction _rotateCameraAction;
    private InputAction _mouseDeltaAction;
    private Vector3 _velocity = Vector3.zero;
    private bool _rotating = true;

    private void Start()
    { 
        _transform = transform;

        _zoomAction = S.I.IM.PC.World.Zoom;
        _rotateCameraAction = S.I.IM.PC.World.RotateCamera;
        _mouseDeltaAction = S.I.IM.PC.World.MouseDelta;

        S.I.IM.PC.World.RotateCamera.started += ToggleRotation;

        GetVectors();
    }

    private void OnDisable()
    {
        S.I.IM.PC.World.RotateCamera.started -= ToggleRotation;
    }

    private void Update()
    {
        _transform.position = Vector3.SmoothDamp(
            _transform.position,
            _playerTransform.position,
            ref _velocity,
            _smoothTime,
            /*_maxSpeed*/Mathf.Infinity,
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
            if (_rotating/*_rotateCameraAction.IsPressed()*/)
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
        _forward = _transform.forward;
        _right = _transform.right;

        // Project the forward and right vectors onto the horizontal plane (y = 0)
        _forward.y = 0f;
        _right.y = 0f;

        // Normalize them
        _forward.Normalize();
        _right.Normalize();

        // Send the forward and right vectors to player movement script.
        // Is the right vector necessary? 
        OnRotatedCamera?.Invoke(_forward, _right);
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