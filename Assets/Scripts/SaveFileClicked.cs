using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveFileClicked : MonoBehaviour
{
    public int saveFileId;
    public SaveFileData data;
    [SerializeField] private LoadSaveFiles saveFiles;

    private string[] levels = {
        "Tutorial",
        "Level1"
    };

    public void Clicked()
    {
        if (data == null)
        {
            SQLiteAPI api = SQLiteAPI.GetAPI();
            api.CreateSaveFile("Save File " + (saveFileId + 1));
            saveFiles.Start();
        }
        else
        { 
            RunDataHandler.saveFileData = data;
            RunDataHandler.LoadRunData();
            Debug.Log(RunDataHandler.runData.floor);
            Debug.Log("Loading : " + levels[RunDataHandler.runData.floor]);
            SceneManager.LoadScene( levels[ RunDataHandler.runData.floor ] );
        }
    }


    public void DeleteSaveFile()
    {
        SQLiteAPI api = SQLiteAPI.GetAPI();
        api.DeleteSaveFile("Save File " + (saveFileId + 1));
        saveFiles.Start();
    }
}
