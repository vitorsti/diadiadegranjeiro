using System.Collections.Generic;
using UnityEngine;
using EventProperties;

[CreateAssetMenu]
public class ListOfEvents : ScriptableObject
{
    public GameEventType type;
    public List<GameObject> Events = new List<GameObject>();
}
