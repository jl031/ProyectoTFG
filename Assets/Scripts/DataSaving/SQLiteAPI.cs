using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mono.Data.Sqlite;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class SQLiteAPI 
{
    public static SQLiteAPI instance;

    private String getDbPath(){
        return "URI=file:" + Application.streamingAssetsPath + "/Database.db";
    }


    public void CreateSchema(){
        IDbConnection connection = new SqliteConnection( getDbPath() );
        connection.Open();
        
        // CREATE RUN TABLE (MUST BE FIRST)
        IDbCommand createRunTable = connection.CreateCommand();
        createRunTable.CommandText = @"CREATE TABLE IF NOT EXISTS run(
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            health REAL NOT NULL,
            shield REAL NOT NULL,
            money INT UNSIGNED NOT NULL,
            enemies_killed INT UNSIGNED NOT NULL,
            damage_dealt REAL NOT NULL,
            damage_taken REAL UNSIGNED NOT NULL,
            score INT UNSIGNED NOT NULL,
            floor INT UNSIGNED NOT NULL,
            finished BOOLEAN NOT NULL,
            win BOOLEAN NOT NULL,
            difficulty INT UNSIGNED NOT NULL
        );";
        createRunTable.ExecuteReader();
        

        // CREATE SAVE FILE TABLE
        IDbCommand createSaveFile = connection.CreateCommand();
        createSaveFile.CommandText = @"CREATE TABLE IF NOT EXISTS save_file(
            name VARCHAR(20) PRIMARY KEY,
            run_in_progress INTEGER,
            FOREIGN KEY(run_in_progress) REFERENCES run(id)
        );";
        createSaveFile.ExecuteReader();

        // CREATE RUN ITEMS TABLE
        IDbCommand createRunItemsTable = connection.CreateCommand();
        createRunItemsTable.CommandText = @"CREATE TABLE IF NOT EXISTS run_items(
            item_id INT UNSIGNED,
            run_id INTEGER,
            quantity INT UNSIGNED NOT NULL,
            PRIMARY KEY(item_id, run_id),
            FOREIGN KEY(run_id) REFERENCES run(id)
        );";
        createRunItemsTable.ExecuteReader();


        connection.Close();
    }

    public static SQLiteAPI GetAPI()
    {
        if (instance == null)
        {
            instance = new SQLiteAPI();
        }

        return instance;
    }

    public List<SaveFileData> GetSaveFilesData()
    {
        List<SaveFileData> files = new List<SaveFileData>();
        IDbConnection connection = new SqliteConnection( getDbPath() );
        connection.Open();

        IDbCommand readSaveFiles = connection.CreateCommand();
        readSaveFiles.CommandText = "SELECT name, run_in_progress FROM save_file;";
        IDataReader reader = readSaveFiles.ExecuteReader();

        while (reader.Read())
        {
            SaveFileData scriptableObjectData = ScriptableObject.CreateInstance<SaveFileData>();
            scriptableObjectData.fileName = reader.GetString(0);

            if (!reader.IsDBNull(1))
            {
                scriptableObjectData.gameInProgress = (ulong) reader.GetInt64(1);
            }

            files.Add( scriptableObjectData );
        }

        connection.Close();
        return files;
    }

    public bool CreateSaveFile(string name)
    { 
        IDbConnection connection = new SqliteConnection( getDbPath() );
        connection.Open();

        IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT name, run_in_progress FROM save_file WHERE name = '"+name+"';";
        IDataReader reader = command.ExecuteReader();
        if (reader.Depth > 0) {
            return false;
        }

        IDbCommand insert = connection.CreateCommand();
        insert.CommandText = "INSERT INTO save_file(name, run_in_progress) VALUES('"+name+"', NULL)";
        insert.ExecuteReader();

        connection.Close();
        return true;
    }

    public void DeleteSaveFile(string name)
    { 
        IDbConnection connection = new SqliteConnection( getDbPath() );
        connection.Open();

        IDbCommand delete = connection.CreateCommand();
        delete.CommandText = "DELETE FROM save_file WHERE name='" + name + "';";
        delete.ExecuteReader();

        connection.Close();
    }

    public void WriteRunData(RunData runData, string saveFileName) {
        DeleteRunData(runData.id);

        IDbConnection connection = new SqliteConnection( getDbPath() );
        connection.Open();

        IDbCommand writeCommand = connection.CreateCommand();
        writeCommand.CommandText = @$"
            INSERT INTO run(
                id,
                health,
                shield,
                money,
                enemies_killed,
                damage_dealt,
                damage_taken,
                score,
                floor,
                finished,
                win,
                difficulty
            ) VALUES(
                {runData.id},
                {runData.health},
                {runData.shield},
                {runData.money},
                {runData.enemies_killed},
                {runData.damage_dealt},
                {runData.damage_taken},
                {runData.score},
                {runData.floor},
                {runData.finished},
                {runData.win},
                {runData.difficulty}
            )
        ";

        writeCommand.ExecuteReader();

        foreach (RunItem item in runData.runItems) {
            IDbCommand writeItemCommand = connection.CreateCommand();
            writeItemCommand.CommandText = $"INSERT INTO run_items(item_id, run_id, quantity) VALUES( {item.itemId}, {runData.id}, 1 )";
            writeItemCommand.ExecuteReader();
        }

        if (!runData.finished)
        {
            IDbCommand linkSaveFile = connection.CreateCommand();
            linkSaveFile.CommandText = " UPDATE save_file SET run_in_progress = " + runData.id + " WHERE name ='" + saveFileName + "';";
            linkSaveFile.ExecuteReader();
        }
        else
        {
            IDbCommand linkSaveFile = connection.CreateCommand();
            linkSaveFile.CommandText = " UPDATE save_file SET run_in_progress = NULL WHERE name ='" + saveFileName + "';";
            linkSaveFile.ExecuteReader();
        }

        connection.Close();
    }

    private void DeleteRunData( ulong id )
    {
        IDbConnection connection = new SqliteConnection( getDbPath() );
        connection.Open();

        IDbCommand deleteCommand = connection.CreateCommand();
        deleteCommand.CommandText = " DELETE FROM run_items WHERE run_id = " + id;
        deleteCommand.ExecuteReader();

        IDbCommand deleteRunCommand = connection.CreateCommand();
        deleteRunCommand.CommandText = " DELETE FROM run WHERE id = " + id;
        deleteRunCommand.ExecuteReader();

        connection.Close();
    }

    public RunData GetRunData( ulong runId)
    {
        RunData runData = ScriptableObject.CreateInstance<RunData>();

        IDbConnection connection = new SqliteConnection( getDbPath() );
        connection.Open();

        IDbCommand readCommand = connection.CreateCommand();
        readCommand.CommandText = @"SELECT id, health, shield, money, enemies_killed, damage_dealt, damage_taken, score, floor, finished, win, difficulty
        FROM run WHERE id = "+runId;

        IDataReader reader = readCommand.ExecuteReader();
        
        while (reader.Read()) {
            runData.id = (ulong) reader.GetInt64(0);
            runData.health = reader.GetFloat(1);
            runData.shield = reader.GetFloat(2);
            runData.money = reader.GetInt32(3);
            runData.enemies_killed = reader.GetInt32(4);
            runData.damage_dealt = reader.GetFloat(5);
            runData.damage_taken = reader.GetFloat(6);
            runData.score = reader.GetInt32(7);
            runData.floor = reader.GetInt32(8);
            runData.win = reader.GetBoolean(9);
            runData.difficulty = reader.GetInt32(10);

        }

        IDbCommand readItems = connection.CreateCommand();
        readItems.CommandText = " SELECT item_id, quantity FROM run_items WHERE run_id = "+runId;

        IDataReader itemReader = readItems.ExecuteReader();
        while (itemReader.Read()){
            RunItem item = ScriptableObject.CreateInstance<RunItem>();
            item.itemId = itemReader.GetInt32(0);
            runData.runItems.Add( item );
        }

        connection.Close();

        return runData;
    }
    private SQLiteAPI()
    {

    } 

}
