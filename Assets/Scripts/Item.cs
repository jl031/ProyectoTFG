using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public Texture sprite;
    public abstract string Description { get; }
    public abstract string ItemName { get; }
    public abstract void OnPickup();
    public abstract void OnRemoval();
    public abstract void Activate();
}
