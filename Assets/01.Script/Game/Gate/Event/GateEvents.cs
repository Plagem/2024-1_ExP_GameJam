namespace _01.Script.Game.Gate.Event
{
    public static class GateEvents
    {
        public static GateEvent EmptyGateEvent = new GateEvent("Empty");
        public static GateEvent MonsterGateEvent = new GateEvent("Monster");
        public static GateEvent DoorGateEvent = new GateEvent("Door");
        public static GateEvent FailGateEvent = new GateEvent("Fail");

        public static GateEvent[] GateEventList = new[] { EmptyGateEvent, MonsterGateEvent, DoorGateEvent, FailGateEvent};
    }
}