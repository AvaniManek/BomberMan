using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gameOverMessageText;

    // Start is called before the first frame update
    void Start()
    {
        SetGameOverMessage();
    }

    private void SetGameOverMessage() 
    {
        if (GameManager.Instance.GameStatus == GameState.Lost)
        {
            gameOverMessageText.text = "Game Over!!!";
        }
        else if (GameManager.Instance.GameStatus == GameState.Won)
        {
            gameOverMessageText.text = "You Won!!!";
        }
    }

    public void OnRetry() 
    {
        SceneManager.LoadScene("Main");
    }
}
