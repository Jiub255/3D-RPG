using UnityEngine;

public class DirectionalLight : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _transform.LookAt(Vector3.zero);
    }
}