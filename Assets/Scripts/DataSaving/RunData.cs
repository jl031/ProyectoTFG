using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunData", menuName = "Scriptable Objects/RunData")]
public class RunData : ScriptableObject
{
    public ulong id;
    public float health;
    public float shield;
    public int money;
    public int enemies_killed;
    public float damage_dealt;
    public float damage_taken;
    public int score;
    public int floor;
    public bool finished;
    public bool win;
    public int difficulty;
    public List<RunItem> runItems = new List<RunItem>();
}
