using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveFileClicked : MonoBehaviour
{
    public int saveFileId;
    public SaveFileData data;
    private Image image;

    [SerializeField] private LoadSaveFiles saveFiles;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void Clicked()
    {
        if (data == null)
        {
            SQLiteAPI api = SQLiteAPI.GetAPI();
            api.CreateSaveFile("Save File " + (saveFileId + 1));
            saveFiles.Start();
        }
        else if (data.gameInProgress <= 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }


    public void DeleteSaveFile()
    {
        SQLiteAPI api = SQLiteAPI.GetAPI();
        api.DeleteSaveFile("Save File " + (saveFileId + 1));
        saveFiles.Start();
    }
}
