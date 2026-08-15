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
}