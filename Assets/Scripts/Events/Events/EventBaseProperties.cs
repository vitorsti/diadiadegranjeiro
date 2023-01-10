using EventProperties;
using System;
using System.Collections.Generic;

[Serializable]
public class EventBaseProperties
{
    public GameEventType eventType;
    public Wheather[] wheather;
    public List<Region> regions;
    public EventDuration duracao;
    public bool spawned;
}
