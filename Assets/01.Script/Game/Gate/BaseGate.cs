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

    public enum GateState
    {
        Close, Focus, Open, Clear
    }
    
    // TODO : 상태 관리 바꿀수있으면 바꾸기
    private GateState state = GateState.Close;

    public GateState State
    {
        get => state;
        set
        {
            state = value;
            switch (value)
            {
                case GateState.Close:
                    break;
                case GateState.Focus:
                    break;
                case GateState.Open:
                    break;
                case GateState.Clear:
                    break;
            }

            FocusObject.SetActive(value == GateState.Focus);
        }
    }

    private GameObject FocusObject;
    
    
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
        State = GateState.Open;
        GateEvent.OnOpen.Invoke(this);
    }

    public void Clear()
    {
        Debug.Log("Clear");
        State = GateState.Clear;
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
        
        Debug.Log($"Clicked {name} s: {state}");

        switch (State)
        {
            case GateState.Close:
                fm = GameManager.Instance.FloorManager;
                fm.Focus(this);
                break;
            case GateState.Focus:
                OnOpen();
                break;
            case GateState.Open:
                break;
            case GateState.Clear:
                // fm.GoNextFloor(); 자동으로 넘어감
                break;
            
        }
        
    }

    public void Summon(MonsterBase monsterBase)
    {
        
    }
}
