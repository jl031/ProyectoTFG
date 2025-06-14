using System;
using UnityEngine;

public class RunDataHandler
{
    public static SaveFileData saveFileData;
    public static RunData runData;

    public static void SaveRunData()
    {
        if (runData == null || runData.id == 0)
        {
            LoadRunData();
        }
        SQLiteAPI.GetAPI().WriteRunData(runData, saveFileData.fileName);
    }

    public static void LoadRunData()
    {
        if (saveFileData.gameInProgress <= 0)
        {
            runData = ScriptableObject.CreateInstance<RunData>();
            runData.id = (ulong) DateTime.Now.Ticks;
            return;
        }


        // TODO: Read the data
        runData = SQLiteAPI.GetAPI().GetRunData(saveFileData.gameInProgress);

    }

    public static void EndGame()
    {
        runData.finished = true;
        SaveRunData();
    }
}
