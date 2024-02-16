using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private string IngameSceneName = "GameScene";

    private void Awake()
    {
        GameManager gm = GameManager.Instance;
    }

    public void Start()
    {
        SoundManager.Instance.Play("bgm_1_mastered",SoundManager.SoundType.BGM);
    }

    public void GameStart()
    {
        SoundManager.Instance.Play("1. touch");
        // 인게임 씬 이름 : GameScene
        StartCoroutine(LoadIngameScene());
    }

    IEnumerator LoadIngameScene()
    {
        AsyncOperation asyncLoad =  SceneManager.LoadSceneAsync(IngameSceneName);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ExitGame()
    {
        SoundManager.Instance.Play("1. touch");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenOption()
    {
        SoundManager.Instance.Play("1. touch");
        return;
    }
}
