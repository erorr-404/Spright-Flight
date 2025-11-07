using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] public float scoreMultiplier = 10f;
    [SerializeField] public TMP_Text scoreText;

    private float _elapsedTime = 0f;
    public int score = 0;
    public bool playerAlive = true;

    private void Update()
    {
        if (playerAlive)
        {
            _elapsedTime += Time.deltaTime;
            score = Mathf.FloorToInt(_elapsedTime * scoreMultiplier);
            scoreText.text = "Score: " + score;
        }
    }

    public void OnPlayerDied()
    {
        playerAlive = false;
    }
}