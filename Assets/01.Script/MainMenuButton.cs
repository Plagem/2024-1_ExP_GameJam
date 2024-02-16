using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    private string IngameSceneName = "GameScene";

    public void GameStart()
    {
        // �ΰ��� �� �̸� : GameScene
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
