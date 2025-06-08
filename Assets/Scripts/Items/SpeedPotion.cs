using UnityEngine;

public class SpeedPotion : Item
{
    public override string Description {
        get => "+1 Speed";
    }

    public override string ItemName
    {
        get => "Speed potion";
    }

    public override int ItemId
    {
        get => 1;
    }

    public override void Activate()
    {
        Player.instance.playerStats.walkspeed += 1f;
    }

    public override void OnPickup()
    {
    }

    public override void OnRemoval()
    {
    }
}
