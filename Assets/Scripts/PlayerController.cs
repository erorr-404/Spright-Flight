using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("PLayer Behaviour")]
    [SerializeField] public float thrustForce = 1f;
    [SerializeField] public float maxSpeed = 5f;
    [SerializeField] public bool destroyOnCollision = true;
    
    [Header("Objects Connection")]
    [SerializeField] public GameObject boosterFlame;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public GameObject explosionEffect;
    [SerializeField] public AudioManager audioManager;
    [SerializeField] public RestartButton restartButton;
    [SerializeField] public Score scoreManager;

    // Update is called once per frame
    void Update()
    {
        // move player
        MovePlayer();

        // draw rocket fire
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        } 
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }
    }

    void MovePlayer()
    {
        // Rotate toward mouse
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));
        Vector2 dir = (worldPos - transform.position).normalized;
        transform.up = dir;
        
        // Move player in mouse direction
        if (Input.GetMouseButton(0))
        {
            rb.AddForce(dir * thrustForce);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (destroyOnCollision)
        {
            scoreManager.OnPlayerDied();
            restartButton.OnPlayerDied();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            audioManager.PlaySFX(audioManager.destroy, audioManager.defaultVolume);
            Destroy(gameObject);
        }
    }
}
