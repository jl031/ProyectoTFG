using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Texture2D sprite;
    public abstract string Description { get; }
    public abstract string ItemName { get; }
    public abstract int ItemId { get; }
    public abstract void OnPickup();
    public abstract void OnRemoval();
    public abstract void Activate();

    public void AddToList()
    {
        RunItem runItem = ScriptableObject.CreateInstance<RunItem>();
        runItem.itemId = ItemId;
        runItem.itemName = ItemName;
        runItem.runId = (ulong) RunDataHandler.runData.id;
        RunDataHandler.runData.runItems.Add(runItem);
    }
}
