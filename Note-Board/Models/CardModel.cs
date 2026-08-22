using Microsoft.Data.Sqlite;

namespace Note_Board.Models;
public class CardModel
{
    public void Create()
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = 
        @"INSERT INTO card (title, colour, status, location_x, location_y)
        VALUES ('New Card', '#FFF697', 'active', 150, 10);";
        command.ExecuteNonQuery();
        connection.Close();
    }

    public string Read(int cardId, string column)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"SELECT $column FROM card WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$column", column);
        command.Parameters.AddWithValue("$card_id", cardId);
        var readCard = command.ExecuteReader();
        readCard.Read();
        return readCard.GetString(0);
    }

    public void UpdateDetails(int cardId, string column, string data)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE card SET $column = $data WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$column", column);
        command.Parameters.AddWithValue("$data", data);
        command.Parameters.AddWithValue("$card_id", cardId);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void UpdatePosition(int cardId, int positionX, int positionY)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"UPDATE card SET position_X = $position_x, position_y = $position_y WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$position_x", positionX);
        command.Parameters.AddWithValue("$position_y", positionY);
        command.Parameters.AddWithValue("$card_id", cardId);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int cardId)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = @"DELETE FROM card WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$card_id", cardId);
        command.ExecuteNonQuery();
        connection.Close();
    }
}