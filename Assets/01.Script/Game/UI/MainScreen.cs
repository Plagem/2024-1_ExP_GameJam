using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject credit;

    [SerializeField]
    private GameObject inventory;

    private void Start()
    {
        inventory.SetActive(false);
        GameManager.Instance.IngameUIManager.isGameClickDisabled = true;
    }

    public void StartGame()
    {
        inventory.SetActive(true);
        this.gameObject.SetActive(false);
        GameManager.Instance.IngameUIManager.isGameClickDisabled = false;
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
        credit.SetActive(true);
    }

    public void OffCredit()
    {
        credit.SetActive(false);
    }
}
