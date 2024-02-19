using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject credit;

    [SerializeField]
    private GameObject inventory;
    [SerializeField]
    private GameObject floorText;
    [SerializeField]
    private GameObject pauseKey;
    [SerializeField]
    private GameObject howto;
    private void Start()
    {
        inventory.SetActive(false);
        floorText.SetActive(false);
        pauseKey.SetActive(false);
        GameManager gm = GameManager.Instance;
        gm.InitGameScene();
        GameManager.Instance.IngameUIManager.isGameClickDisabled = true;
        
        SoundManager.Instance.Play("bgm_1_mastered", SoundManager.SoundType.BGM);
        
        
        if(PlayerPrefs.GetInt("IsRestart") == 1)
        {
            StartGame();
            PlayerPrefs.SetInt("IsRestart", 0);
        }
    }

    public void StartGame()
    {
        
        inventory.SetActive(true);
        floorText.SetActive(true);
        pauseKey.SetActive(true);
        
        this.gameObject.SetActive(false);
        GameManager.Instance.IngameUIManager.isGameClickDisabled = false;
    }

    public void PlaySound()
    {
        SoundManager.Instance.Play("1. touch");
    }

    
    
    public void Exitgame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void Credit()
    {
        // SoundManager.Instance.Play("1. touch");
        credit.SetActive(true);
    }

    public void OffCredit()
    {
        SoundManager.Instance.Play("1. touch");
        credit.SetActive(false);
    }
    
    public void Howto()
    {
        // SoundManager.Instance.Play("1. touch");
        howto.SetActive(true);
    }

    public void OffHoto()
    {
        SoundManager.Instance.Play("1. touch");
        howto.SetActive(false);
    }
}
