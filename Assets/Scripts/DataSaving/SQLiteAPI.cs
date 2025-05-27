using System;
using System.Data;
using Mono.Data.Sqlite;
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
            health INT UNSIGNED NOT NULL,
            shield INT UNSIGNED NOT NULL,
            money INT UNSIGNED NOT NULL,
            enemies_killed INT UNSIGNED NOT NULL,
            damage_dealt INT UNSIGNED NOT NULL,
            damage_taken INT UNSIGNED NOT NULL,
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

    public static SQLiteAPI GetAPI(){
        if (instance == null) {
            instance = new SQLiteAPI();
        }

        return instance;
    }

    private SQLiteAPI(){

    } 

}
