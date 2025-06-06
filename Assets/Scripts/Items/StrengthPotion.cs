using UnityEngine;

public class StrengthPotion : Item
{

    public override string ItemName  {
        get => "Strength Potion"; 
    }
    public override string Description {
        get => "+1 Damage";
    }

    public override int ItemId
    {
        get => 1;
    } 

    public override void Activate()
    {
        if (Player.instance.playerStats != null)
        {
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
