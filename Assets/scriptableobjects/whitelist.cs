using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "list", menuName = "listRace")]
public class whitelist : ScriptableObject
{
    public List<races> Races = new List<races>();

}
