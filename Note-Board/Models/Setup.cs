using Microsoft.Data.Sqlite;

namespace Note_Board.Models;
public class Setup
{
    public Setup()
    {
        Console.WriteLine("Before establsihing connection.");
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "PRAGMA foreign_keys = ON;";
        command.ExecuteNonQuery();
        Console.WriteLine("Executed command for Pragma");

        command.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS card (
            card_id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            type TEXT NOT NULL,
            colour TEXT NOT NULL,
            status TEXT NOT NULL,
            position_x INTEGER NOT NULL,
            position_y INTEGER NOT NULL
        );
       ";
        command.ExecuteNonQuery();
        Console.WriteLine("Execute query to create card");

        command.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS checklist (
            checklist_id INTEGER PRIMARY KEY AUTOINCREMENT,
            card_id INTEGER NOT NULL,
            item TEXT NOT NULL,
            status TEXT NOT NULL,
            FOREIGN KEY(card_id) REFERENCES card(card_id) ON DELETE CASCADE
        );
        ";
        command.ExecuteNonQuery();
        Console.WriteLine("Execute query to create checklist");

        command.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS countdown (
            countdown_id INTEGER PRIMARY KEY AUTOINCREMENT,
            card_id INTEGER NOT NULL,
            date_time TEXT NOT NULL,
            FOREIGN KEY(card_id) REFERENCES card(card_id) ON DELETE CASCADE
        );
        ";
        command.ExecuteNonQuery();
        Console.WriteLine("Execute query to create countdown");

        command.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS note (
            note_id INTEGER PRIMARY KEY AUTOINCREMENT,
            card_id INTEGER NOT NULL,
            note TEXT NOT NULL,
            FOREIGN KEY(card_id) REFERENCES card(card_id) ON DELETE CASCADE
        );
        ";

        connection.Close();
    }
}