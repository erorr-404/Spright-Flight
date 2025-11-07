using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] public Button restartButton;
    
    void Start()
    {
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(ReloadScene);
    }

    public void OnPlayerDied()
    {
        restartButton.gameObject.SetActive(true);
    }
    
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
