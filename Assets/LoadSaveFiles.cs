using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSaveFiles : MonoBehaviour
{
    public static string loadedFile = "";
    public GameObject file1;
    public GameObject file2;
    public GameObject file3;


    void Start()
    {
        SQLiteAPI api = SQLiteAPI.GetAPI();
        api.CreateSchema();
        List<SaveFileData> data = api.GetSaveFilesData();

        if (data.Count > 0)
        {
            LoadFileData(data[0], file1);
        }
        if (data.Count > 1)
        {
            LoadFileData(data[1], file1);
        }
        if (data.Count > 2)
        {
            LoadFileData(data[2], file1);
        }
    }

    void LoadFileData(SaveFileData fileData, GameObject file)
    {
        Debug.Log(fileData.fileName + " " + file.name);
        GameObject fileContainer = file.transform.Find("NewGame").gameObject;

        SaveFileClicked clickedScript = file.GetComponent<SaveFileClicked>();
        clickedScript.data = fileData;

        Text fileText = fileContainer.GetComponent<Text>();
        fileText.text = fileData.fileName;

        if (fileData.gameInProgress == 0)
        {
            fileText.text += " \nNew Game";
        }
        else
        {
            fileText.text += " \nContinue Game";
        }

    }
}
