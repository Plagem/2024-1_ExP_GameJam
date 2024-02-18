using UnityEngine;

namespace _01.Script.Game.Gate.Event
{
    public static class GateEvents
    {
        public static GateEvent EmptyGateEvent = new GateEvent("Empty").
            SetOpenEvent(gate=> gate.Clear());


        public static GateEvent MonsterGateEvent = new GateEvent("Monster").SetOpenEvent(gate =>
        {
            GameObject monsterObj = GameObject.Instantiate(gate.fm.MonsterPrefeb, gate.transform);
            monsterObj.GetComponent<Monster>().gate = gate;
            SoundManager.Instance.Play("11. monster_appears");
        }).SetEventMessage("몬스터가 문을 터뜨려버렸다..");
    



        public static GateEvent DoorGateEvent = new GateEvent("Door").

            SetOpenEvent(gate=>
            {
                GameObject doorObj = GameObject.Instantiate(gate.fm.DoorPrefeb, gate.transform);
                SoundManager.Instance.Play("6. door_appears");
            }).SetEventMessage("문이 도망가면서 문을 잠가버렸다..");
        
        public static GateEvent RareDoorGateEvent = new GateEvent("RareDoor").
            SetOpenEvent(gate =>
            {
                GameObject rareDoorObj = GameObject.Instantiate(gate.fm.DoorPrefeb, gate.transform);
                rareDoorObj.GetComponent<DoorMonsterBase>().isRare = true;
                SoundManager.Instance.Play("6. door_appears");
            }).SetEventMessage("문이 도망가면서 문을 잠가버렸다..");
        
        public static GateEvent FailGateEvent = new GateEvent("Fail").
            SetOpenEvent((gate) =>
            {
                GameObject bObj = GameObject.Instantiate(gate.fm.BearPrefab, gate.transform);
                Bear bear = bObj.GetComponent<Bear>();
                SoundManager.Instance.Play("4. bear_roar");
                bear.StartCoroutine(bear.BearRoutine(1,3));
            }).SetEventMessage("문인 척 하는 곰에게 당했다..");

        public static GateEvent[] GateEventList = new[] {FailGateEvent , MonsterGateEvent, DoorGateEvent, RareDoorGateEvent, EmptyGateEvent};
    }
}