
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    MainScreen GameStartScene;
    [SerializeField]
    GameObject SettingPannel;

    private GameObject pauseKey;

    private void Start()
    {
        pauseKey = GameManager.Instance.IngameUIManager.pauseKey;
    }

    public void GameStart()
    {
        SoundManager.Instance.Play("1. touch");
        // 인게임 씬 이름 : GameScene
        StartCoroutine(ToMain());
    }

    public void GamePause()
    {
        SoundManager.Instance.Play("1. touch");
        SettingPannel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameResume()
    {
        SoundManager.Instance.Play("1. touch");
        SettingPannel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SoundManager.Instance.Play("1. touch");
        PlayerPrefs.SetInt("IsRestart", 1);
        StartCoroutine(ToMain());
    }

    public void Mainmenu()
    {
        SoundManager.Instance.Play("1. touch");
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
