using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string NAME = "@Game";
    private static GameManager instance;

    public FloorManager FloorManager;

    public static GameManager Instance
    {
        get
        {
            // 없을경우 생성
            if (instance == null)
            {
                GameObject root = GameObject.Find(NAME);
                if (root == null)
                {
                    root = new GameObject { name = NAME };
                }

                instance = root.AddComponent<GameManager>();
            }

            return instance;
        }
    }

    public GameState gameState = GameState.Start;


    public void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainScene":
                gameState = GameState.Start;
                break;
            case "GameScene":
                gameState = GameState.Game;
                StartGame(); // 테스트용
                break;
        }
        DontDestroyOnLoad(this);
    }

    public void StartGame()
    {
        if (gameState == GameState.Start)
        {
            SceneManager.LoadScene("GameScene");
            gameState = GameState.Game;
        }

        FloorManager = GameObject.Find("@FloorManager").GetComponent<FloorManager>();
        FloorManager.GoFloor(1);
    }


    public void Fail()
    {
        SceneManager.LoadScene("StartScene");
    }
}


public enum GameState
{
    Start,
    Game
}