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
    [SerializeField] public float scoreMultiplier = 10f;
    
    [Header("Objects Connection")]
    [SerializeField] public GameObject boosterFlame;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public UIDocument uiDocument;
    [SerializeField] public GameObject explosionEffect;
    [SerializeField] public AudioManager audioManager;

    private float _elapsedTime = 0f;
    private int _score = 0;
    private Label _scoreText;
    private Button _restartButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        _restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        _restartButton.style.display = DisplayStyle.None;
        _restartButton.clicked += ReloadScene;
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
        _elapsedTime += Time.deltaTime;
        _score = Mathf.FloorToInt(_elapsedTime * scoreMultiplier);
        _scoreText.text = "Score: " + _score;
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
            Instantiate(explosionEffect, transform.position, transform.rotation);
            audioManager.PlaySFX(audioManager.destroy, audioManager.defaultVolume);
            _restartButton.style.display = DisplayStyle.Flex;
            Destroy(gameObject);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
