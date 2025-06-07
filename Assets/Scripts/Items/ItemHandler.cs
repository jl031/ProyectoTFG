using System.Collections.Generic;
using UnityEngine;

public class ItemHandler
{
    List<Item> runItems;
    private static ItemHandler _instance;
    private static GameObject _parent;

    public static ItemHandler GetItemHandler()
    {
        if (_instance == null)
        {
            _parent = new GameObject("itemHandler");
            _instance = new ItemHandler();
        }
        return _instance;
    }

    public Item GetItem(int id)
    {
        if (id >= 0 && id < runItems.Count)
        {
            return runItems[id];
        }

        return null;  
    }

    private ItemHandler()
    {
        this.runItems = new List<Item>
        {
            _parent.AddComponent<StrengthPotion>(),
            _parent.AddComponent<SpeedPotion>()
        };
    }

}
