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
    public PolygonCollider2D Collider;
    [SerializeField] private Material focusMaterial;
    
    SpriteRenderer sr;

    public enum GateState
    {
        Close, Focus, Opening, Opened, Clear
    }
    
    // TODO : 상태 관리 바꿀수있으면 바꾸기
    private GateState state = GateState.Close;

    public GateState State
    {
        get => state;
        set
        {
            state = value;
            // switch (value) 함수에서 관리함.
            // {
            //     case GateState.Close:
            //         break;
            //     case GateState.Focus:
            //         break;
            //     case GateState.Open:
            //         break;
            //     case GateState.Clear:
            //         break;
            // }

            FocusObject.SetActive(value == GateState.Focus);
        }
    }
    [SerializeField]
    private GameObject FocusObject;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        FocusObject = transform.GetChild(0).gameObject;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = CloseSprite;
        Collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {

        Collider.enabled = true;
        sr.enabled = true;
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

        Collider.enabled = false;
        State = GateState.Opening;
        sr.sprite = OpenSprite;
        GateEvent.OnOpen.Invoke(this);
        
        StartCoroutine(Utility.WaitExecute(0.4f, () =>
        {
            sr.enabled = false;
            State = GateState.Opened;
        }));

        
    }

    public void Clear()
    {
        Debug.Log("Clear");
        State = GateState.Clear;
        fm.FloorCleared();
    }

    public void OnMouseDown()
    {
        UIManager iuiM = gm.IngameUIManager;
        if (iuiM.UiHoverListener.isUIOverride)
            return;
        fm = GameManager.Instance.FloorManager;
        if (iuiM.isGameClickDisabled || !fm.IsGateClickable)
            return;
        
        Debug.Log($"Clicked {name} s: {state}");

        switch (State)
        {
            case GateState.Close:
                fm.Focus(this);
                break;
            case GateState.Focus:
                OnOpen();
                break;
            case GateState.Opening:
            case GateState.Opened:
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
