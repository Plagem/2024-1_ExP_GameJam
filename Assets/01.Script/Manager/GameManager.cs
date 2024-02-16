using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string NAME = "@Game";
    private static GameManager instance;

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

    public GameState gameState = GameState.Main;


    public void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "MainScene":
                gameState = GameState.Main;
                break;
            case "GameScene":
                gameState = GameState.Game;
                break;
        }
        DontDestroyOnLoad(this);
    }

    
}


public enum GameState
{
    Main,
    Game
}