using Microsoft.Data.Sqlite;

namespace Note_Board.Models;
public class Checklist : Card
{   
    private int cardId;
    
    public Checklist(int id) : base(id)
    {
        cardId = id;
    }

    public string GetProgress()
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        
        command.CommandText = "SELECT SUM(status) FROM checklist WHERE card_id = $card_id AND status = 'completed';";
        command.Parameters.AddWithValue("$card_id", cardId);
        var readChecklist = command.ExecuteReader();
        int completed = readChecklist.GetInt32(0);

        command.CommandText = "SELECT SUM(status) FROM checklist WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$card_id", cardId);
        readChecklist = command.ExecuteReader();
        int total = readChecklist.GetInt32(0);
        
        connection.Close();

        string progress = "Progress: " + completed.ToString() + " / " + total.ToString();
        return progress;
    }

    public void AddItem(string item)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO checklist (item, status) VALUES ($item, 'pending');";
        command.Parameters.AddWithValue("$item", item);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void DeleteItem(int itemId)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM checklist WHERE item_id = $item_id;";
        command.Parameters.AddWithValue("$item_id", itemId);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void MarkItem(int itemId)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT status FROM checklist WHERE item_id = $item_id;";
        command.Parameters.AddWithValue("$item_id", itemId);
        using var readStatus = command.ExecuteReader();
        if (readStatus.GetString(0).Equals("pending"))
        {
            command.CommandText = "UPDATE checklist SET status = 'completed' WHERE item_id = $item_id;";
        } else
        {
            command.CommandText = "UPDATE checklist SET status = 'pending' WHERE item_id = $item_id;";
        }
        command.Parameters.AddWithValue("$item_id", itemId);
        command.ExecuteNonQuery();
        connection.Close();
    }
}