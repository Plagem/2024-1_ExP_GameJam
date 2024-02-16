using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private string IngameSceneName = "GameScene";

    public void GameStart()
    {
        // 인게임 씬 이름 : GameScene
        StartCoroutine(LoadIngameScene());
    }

    IEnumerator LoadIngameScene()
    {

        yield return StartCoroutine(GameManager.Instance.StartGame());
        // while(!asyncLoad.isDone)
        // {
        //     yield return null;
        // }
        //     yield return null;
        // }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OpenOption()
    {
        return;
    }
}
