using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    public float maxSpeed = 5f;
    public bool destroyOnCollision = true;
    public GameObject boosterFlame;
    private Rigidbody2D rb;
    private float elapsedTime = 0f;
    private int score = 0;
    public float scoreMultiplier = 10f;
    public UIDocument uiDocument;
    private Label scoreText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
    }

    // Update is called once per frame
    void Update()
    {
        // update score
        UpdateScore();
        
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

    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;
    }

    void MovePlayer()
    {
        // Calculate mouse direction
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
        Vector2 direction = (mousePos - transform.position).normalized;
        transform.up = direction;
        
        // Move player in mouse direction
        if (Mouse.current.leftButton.isPressed)
        {
            rb.AddForce(direction * thrustForce);
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
            Destroy(gameObject);
        }
    }
}
