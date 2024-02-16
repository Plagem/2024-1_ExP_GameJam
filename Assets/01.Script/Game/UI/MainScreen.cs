using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject credit;

    public void StartGame()
    {
        this.gameObject.SetActive(false);
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
