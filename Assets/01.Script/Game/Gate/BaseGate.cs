using System;
using System.Collections;
using System.Collections.Generic;
using _01.Script;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseGate : MonoBehaviour
{
    public GameManager gm;
    public FloorManager fm;
    public GateEvent GateEvent;
    public Sprite CloseSprite;
    public Sprite OpenSprite;
    
    
    SpriteRenderer sr;
    
    
    // TODO : 상태 관리 바꿀수있으면 바꾸기
    private bool isFocused;
    private bool isCleared;

    private GameObject FocusObject;

    public bool IsFocused
    {
        get => isFocused;
        set
        {
            isFocused = value;
            FocusObject.SetActive(value);
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        FocusObject = transform.GetChild(0).gameObject;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = CloseSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        isCleared = false;
        IsFocused = false;
        
        sr.color = Color.white;
        sr.sprite = CloseSprite;
    }


    public void OnOpen()
    {
        if (GateEvent == null)
        {
            Debug.LogError("이벤트 지정되지 않음");
            return;
        }

        sr.sprite = OpenSprite;
        IsFocused = false;
        GateEvent.OnOpen.Invoke(this);
    }

    public void Clear()
    {
        Debug.Log("Clear");
        isCleared = true;
        sr.color = Color.yellow;
        fm.FloorCleared();
    }

    public void OnMouseDown()
    {
        UIManager iuiM = gm.IngameUIManager;
        if (iuiM.UiHoverListener.isUIOverride)
            return;
        if (iuiM.isGameClickDisabled)
            return;
        
        Debug.Log($"Clicked {name} c: {isCleared} f:{isFocused}");
        
        if(isCleared)
        {
            fm.GoNextFloor();
            return;
        }
        // 초점 X
        if (!IsFocused)
        {
            fm = GameManager.Instance.FloorManager;
            fm.Focus(this);
            Debug.Log($"Clicked {name} c: {isCleared} f:{isFocused}");
            return;
        }
        // 초점 O
        OnOpen();
    }

    public void Summon(MonsterBase monsterBase)
    {
        
    }
}
