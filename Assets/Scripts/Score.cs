using System;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private float scoreMultiplier = 10f;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private float _elapsedTime = 0f;
    public int score = 0;
    public int highScore = 0;
    private bool _playerAlive = true;

    private void Start()
    {
        ResetScore();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        highScoreText.gameObject.SetActive(false);
        _playerAlive = true;
    }

    private void Update()
    {
        Debug.Log("playerAlive: " + _playerAlive);
        if (!_playerAlive) return;

        _elapsedTime += Time.deltaTime;
        
        if (!_playerAlive) return;

        score = Mathf.FloorToInt(_elapsedTime * scoreMultiplier);
        
        if (!_playerAlive) return;

        scoreText.text = "Score: " + score;
    }

    private void ResetScore()
    {
        _elapsedTime = 0f;
        score = 0;
        scoreText.text = "Score: 0";
        _playerAlive = true;
        highScoreText.gameObject.SetActive(false);
    }


    public void OnPlayerDied()
    {
        if (!_playerAlive) return;
        
        _playerAlive = false;
        highScoreText.gameObject.SetActive(true);

        Debug.Log("Player died.");
        
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
}