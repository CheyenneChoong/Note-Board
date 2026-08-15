using Microsoft.Data.Sqlite;

namespace Note_Board.Models;
public class Countdown : Card
{
    private int cardId;
    private int countdownId;
    private string dateTime;

    public Countdown(int id) : base(id)
    {
        cardId = id;
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM countdown WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$card_id", cardId);

        using var readCountdown = command.ExecuteReader();
        countdownId = readCountdown.GetInt32(0);
        dateTime = readCountdown.GetString(2);

        connection.Close();
    }

    public void UpdateCountdown(string update)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "UPDATE countdown SET date_time = $update WHERE countdown_id = $countdown_id";
        command.Parameters.AddWithValue("$update", update);
        command.Parameters.AddWithValue("$countdown_id", countdownId);
        command.ExecuteNonQuery();
        connection.Close();
    }
}