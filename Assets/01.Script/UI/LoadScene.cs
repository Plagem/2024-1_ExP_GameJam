
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    MainScreen GameStartScene;

    public void GameStart()
    {
        SoundManager.Instance.Play("1. touch");
        // 인게임 씬 이름 : GameScene
        StartCoroutine(ToMain());
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("IsRestart", 1);
        StartCoroutine(ToMain());
    }

    public void Mainmenu()
    {
        PlayerPrefs.SetInt("IsRestart", 0);
        StartCoroutine(ToMain());
    }

    IEnumerator ToMain()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
