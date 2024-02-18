
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _01.Script;
using _01.Script.Game.Gate.Event;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class FloorManager : MonoBehaviour
{
    [Header("곰 프리펩")] public GameObject BearPrefab;

    [Header("문 프리펩")] public GameObject DoorPrefeb;

    [Header("몬스터 프리펩")] public GameObject MonsterPrefeb;

    public List<DoorData> AllDoorDataList;

    public List<float> AllMonsterDataList;

    private int currentFloor = 0;

    public int CurrentFloor
    {
        get => currentFloor;
        set
        {
            currentFloor = value;
            foreach (var floorChangeListener in FloorChangeListeners)
            {
                floorChangeListener.OnFloorChanged(value);
            }
        }
    }
    
    // 귀찮으니까 일단 이렇게
    public bool IsGateClickable => Gates.All((g) =>
        g.State == BaseGate.GateState.Close || g.State == BaseGate.GateState.Focus|| g.State == BaseGate.GateState.Disabled);
    
    [Header("FailGateEvent , MonsterGateEvent, DoorGateEvent, RareDoorGateEvent, EmptyGateEvent")]
    public List<int> Probabliteis;
    public List<BaseGate> Gates;
    [Header("층 바뀜 리스너")] public List<IFloorChangeListener> FloorChangeListeners;
    
    
    public Image FadeImg;

    public void Awake()
    {
        FloorChangeListeners = new List<IFloorChangeListener>();
    }

    public void Start()
    {
        
    }

    public void GoFloor(int floor, bool noFade = false)
    {
        CurrentFloor = floor;
        InitializeFloor();
    }

    /// <summary>
    /// 해당 층이 클리어 될 시 호출되는 함수
    /// </summary>
    public void FloorCleared()
    {
        SoundManager.Instance.Play("3. goto_next_floor");
        GameManager.Instance.IngameUIManager.isGameClickDisabled = true;
        FadeImg.gameObject.SetActive(true);
        StartCoroutine(FadeRoutine(0, 1, 0.7f, () =>
        {
            GoNextFloor();
            StartCoroutine(FadeRoutine(1, 0, 0.7f,
                () =>
                {
                    GameManager.Instance.IngameUIManager.isGameClickDisabled = false;
                    FadeImg.gameObject.SetActive(false);
                }));
        }));
    }

    /// <summary>
    /// 새로운 층에서 문 초기화
    /// </summary>
    public void InitializeFloor()
    {
        for (int i = 0; i < 3; i++)
        {
            int eventIdx = Utility.WeightedRandom(Probabliteis.ToArray());
            Gates[i].init();
            Gates[i].GateEvent = GateEvents.GateEventList[eventIdx];
            Debug.Log($"[{currentFloor}F] init {i} gate : {GateEvents.GateEventList[eventIdx].name}");
        }
    }

    public void GoNextFloor()=>GoFloor(CurrentFloor+1);


    public IEnumerator FadeRoutine(float start, float end, float time, Action callBack = null)
    {
        float currentTime = 0;
        Color color = FadeImg.color;
        while (currentTime < time)
        {
            color.a = Mathf.Lerp(start, end, (currentTime)/time);
            FadeImg.color = color;
            currentTime += Time.deltaTime;
            yield return null;
        }
        if(callBack != null)
            callBack.Invoke();
    }

    public void Focus(BaseGate gate)
    {
        foreach (var _gate in Gates)
        {
            _gate.State = BaseGate.GateState.Close;
        }

        gate.State = BaseGate.GateState.Focus;
    }

    public void ClearEntity()
    {
        if(Bear.bear)
            Destroy(Bear.bear);
        if(MonsterBase.monster)
            Destroy(MonsterBase.monster);
    }
}
