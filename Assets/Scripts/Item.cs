using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public abstract void OnPickup();
    public abstract void OnRemoval();
    public abstract void Activate();
}
