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
    public UIManager IngameUIManager;

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
                break;
        }
        DontDestroyOnLoad(this);
    }
    
    public void InitGameScene()
    {
        FloorManager = GameObject.Find("@FloorManager").GetComponent<FloorManager>();
        IngameUIManager = GameObject.Find("@UIManager").GetComponent<UIManager>();
        FloorManager.FloorChangeListeners.Add(IngameUIManager);
        FloorManager.GoFloor(1);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


public enum GameState
{
    Start,
    Game
}