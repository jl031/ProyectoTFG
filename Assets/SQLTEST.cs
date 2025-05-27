using UnityEngine;

public class SQLTEST : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SQLiteAPI api = SQLiteAPI.GetAPI();
        api.CreateSchema();
    }

}
