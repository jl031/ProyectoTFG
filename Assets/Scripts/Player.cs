using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] Player _player;
    public PlayerController controller;
    public PlayerStats playerStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = _player;

        RunData data = RunDataHandler.runData;
        if (playerStats == null)
        {
            playerStats = ScriptableObject.CreateInstance<PlayerStats>();
            if (data.health > 0)
            {
                playerStats.health = data.health;
            }
        }

        RunDataHandler.SaveRunData();
        LoadItems();
    }

    void LoadItems()
    {
        foreach (RunItem runItem in RunDataHandler.runData.runItems)
        {
            Item item = ItemHandler.GetItemHandler().GetItem(runItem.itemId);
            item.Activate();   
        }
    }

}
