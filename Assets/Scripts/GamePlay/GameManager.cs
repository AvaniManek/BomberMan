using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameState _gameStatus;

    public static GameManager Instance { get => _instance; }
    public GameState GameStatus { get => _gameStatus; set => _gameStatus = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        GridBlock.gameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        GridBlock.gameOver -= OnGameOver;
    }

    

    public void OnGameOver(GameState gameState) 
    {
        _gameStatus = gameState;
    }
}
