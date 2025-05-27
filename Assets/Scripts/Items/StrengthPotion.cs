using UnityEngine;

public class StrengthPotion : Item
{
    public override void Activate()
    {
        if (Player.instance.playerStats != null) {
            Player.instance.playerStats.damage += 1f;
        }
    }

    public override void OnPickup()
    {
    }

    public override void OnRemoval()
    {
        if (Player.instance.playerStats != null) {
            Player.instance.playerStats.damage -= 1f;
        }
    }
}
