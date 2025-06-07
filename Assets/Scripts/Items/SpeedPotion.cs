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
        throw new System.NotImplementedException();
    }

    public override void OnPickup()
    {
        throw new System.NotImplementedException();
    }

    public override void OnRemoval()
    {
        throw new System.NotImplementedException();
    }
}
