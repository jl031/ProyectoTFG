using UnityEngine;

[CreateAssetMenu(fileName = "RunItem", menuName = "Scriptable Objects/RunItem")]
public class RunItem : ScriptableObject
{
    public string itemName;
    public int runId;
    public int itemId;
}
