using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Race", menuName = "Race")]
public class races : ScriptableObject
{
 

    public int idRace;
    public new string name;
    public float velocity;
    public float damage;
    public float ratefire;
    public float life;
    public float jump;
    
}
