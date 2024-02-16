using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string NAME = "@Game";
    private static GameManager instance;

    private int currentFloor = 0;
    public int CurrentFloor => currentFloor;

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

    public void StartGame()
    {
        GoFloor(0);
    }

    public void GoFloor(int floor)
    {
        currentFloor = floor;
        initializeFloor();
    }


    public void initializeFloor()
    {
        
    }

    public void GoNextFloor()=>GoFloor(currentFloor+1);

    public void Fail()
    {
        SceneManager.LoadScene("GameScene");
    }
}


public enum GameState
{
    Main,
    Game
}