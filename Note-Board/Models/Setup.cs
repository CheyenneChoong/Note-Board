using Microsoft.Data.Sqlite;

namespace Note_Board.Models;
public class Setup
{
    public Setup()
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = 
        @"
        PRAGMA foreign_keys = ON;

        CREATE TABLE IF NOT EXISTS card (
            card_id INT PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            description TEXT NOT NULL,
            type TEXT NOT NULL,
            colour TEXT NOT NULL,
            status TEXT NOT NULL,
            position_x INT NOT NULL,
            position_y INT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS checklist (
            checklist_id INT PRIMARY KEY AUTOINCREMENT,
            card_id INT NOT NULL,
            item TEXT NOT NULL,
            status TEXT NOT NULL,
            FOREIGN KEY(card_id) REFERENCES card(card_id) ON DELETE CASCADE
        );

        CREATE TABLE IF NOT EXISTS countdown (
            countdown_id INT PRIMARY KEY AUTOINCREMENT,
            card_id INT NOT NULL,
            date_time TEXT,
            FOREIGN KEY(card_id) REFERENCES card(card_id) ON DELETE CASCADE
        );
        ";
        command.ExecuteNonQuery();
        connection.Close();
    }
}