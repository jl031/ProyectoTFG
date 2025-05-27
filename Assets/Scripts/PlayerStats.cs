using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float damage = 0f;
    public float walkspeed = 1f;
    public float maxHealth = 5f;
    public float health = 5f;
}
