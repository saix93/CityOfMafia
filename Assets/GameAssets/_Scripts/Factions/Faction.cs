using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Faction", menuName = "Factions/Faction", order = 1)]
public class Faction : ScriptableObject
{
    public string factionName;
    public Color color;

    public Faction(Color color)
    {
        this.color = color;
    }
}
