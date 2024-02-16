namespace _01.Script.Game.Gate.Event
{
    public static class GateEvents
    {
        public static GateEvent EmptyGateEvent = new GateEvent("Empty").
            SetOpenEvent(gate=> gate.Clear());
        public static GateEvent MonsterGateEvent = new GateEvent("Monster").
            SetOpenEvent(gate=> gate.Clear());
        public static GateEvent DoorGateEvent = new GateEvent("Door").
            SetOpenEvent(gate=> gate.Clear());
        public static GateEvent FailGateEvent = new GateEvent("Fail").
            SetOpenEvent(gate=> gate.Clear());

        public static GateEvent[] GateEventList = new[] { EmptyGateEvent, MonsterGateEvent, DoorGateEvent, FailGateEvent};
    }
}