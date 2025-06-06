using UnityEngine;

[CreateAssetMenu(fileName = "SaveFileData", menuName = "Scriptable Objects/SaveFileData")]
public class SaveFileData : ScriptableObject
{
    public string fileName;
    public ulong gameInProgress;    
}
