using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSaveFiles : MonoBehaviour
{
    public static string loadedFile = "";
    public GameObject file1;
    public GameObject file2;
    public GameObject file3;


    public void Start()
    {
        SQLiteAPI api = SQLiteAPI.GetAPI();
        api.CreateSchema();
        List<SaveFileData> data = api.GetSaveFilesData();

        foreach (SaveFileData file in data)
        {
            int id = file.fileName[file.fileName.Length - 1] - '0' - 1;
            switch (id)
            {
                case 0:
                    LoadFileData(file, file1);
                    break;
                case 1:
                    LoadFileData(file, file2);
                    break;
                case 2:
                    LoadFileData(file, file3);
                    break;
            }
        }
    }

    

    void LoadFileData(SaveFileData fileData, GameObject file)
    {
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
