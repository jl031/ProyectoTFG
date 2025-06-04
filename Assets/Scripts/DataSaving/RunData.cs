using UnityEngine;

[CreateAssetMenu(fileName = "RunData", menuName = "Scriptable Objects/RunData")]
public class RunData : ScriptableObject
{
    public int id;
    public float health;
    public float shield;
    public int money;
    public int enemies_killed;
    public int damage_dealt;
    public int damage_taken;
    public int score;
    public int floor;
    public bool finished;
    public bool win;
    public int difficulty;
}
