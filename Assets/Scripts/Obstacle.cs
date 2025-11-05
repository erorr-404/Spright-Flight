using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Spawn Properies")]
    [SerializeField] public float minSize = 0.5f;
    [SerializeField] public float maxSize = 2.0f;
    [SerializeField] public float minSpeed = 100f;
    [SerializeField] public float maxSpeed = 500f;
    [SerializeField] public float maxSpinSpeed = 10f;
    
    [Header("Objects Connections")]
    [SerializeField] public AudioManager audioManager;

    public Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        Vector2 randomDirection = Random.insideUnitCircle;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque / randomSize);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        audioManager.PlaySFX(audioManager.meteoriteCollision, 0.1f);
    }
}
