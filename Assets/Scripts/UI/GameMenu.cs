using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    private void OnEnable()
    {
        GridBlock.gameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GridBlock.gameOver -= OnGameOver;
    }

    public void OnGameOver(GameState gameState)
    {
        SceneManager.LoadScene("GameOver");
    }
}
