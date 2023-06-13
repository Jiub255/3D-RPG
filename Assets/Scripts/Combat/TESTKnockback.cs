using System.Collections;
using UnityEngine;

public class TESTKnockback : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private float _knockbackMult = 2f;
    [SerializeField]
    private float _knockbackDuration = 0.5f;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    public void GetKnockedBack(Vector3 knockbackVector)
    {
        _rb.AddForce(knockbackVector * _knockbackMult, ForceMode.Impulse);
        //StartCoroutine(KnockbackCoroutine(knockbackVector));
    }

    private IEnumerator KnockbackCoroutine(Vector3 knockbackVector)
    {
        float timer = 0f;
        while (timer < _knockbackDuration)
        {
            float lerpMult = Mathf.Lerp(1, 0, timer / _knockbackDuration);
            Debug.Log($"Timer: {timer}, LerpMult: {lerpMult}"); 
            _rb.AddForce(knockbackVector * _knockbackMult * lerpMult, ForceMode.Force);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}