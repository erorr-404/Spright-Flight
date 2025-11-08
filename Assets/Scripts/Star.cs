using UnityEngine;
using Random = UnityEngine.Random;

public class Star : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 2f; // how fast it pulses
    [SerializeField] private float scaleAmount = 0.2f; // how much it grows

    private Vector3 _baseScale;
    private float _phaseShift;

    private void Start()
    {
        _baseScale = transform.localScale;
        _phaseShift = Random.Range(0f, 5f);
    }

    void Update()
    {
        float scale = scaleAmount * Mathf.Sin(Time.time * speed + _phaseShift) + 1;
        transform.localScale = _baseScale * scale;
    }
}