using Microsoft.Data.Sqlite;

namespace Note_Board.Models;
public class Card
{
    private int cardId;
    private string title = "";
    private string type = "";
    private string colour = "";
    private string status = "";
    private int positionX = 100;
    private int positionY = 100;

    public Card(int id)
    {
        cardId = id;
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM card WHERE card_id = $card_id";
        command.Parameters.AddWithValue("$card_id", cardId);
        using var readCard = command.ExecuteReader();
        while (readCard.Read())
        {
            title = readCard.GetString(1);
            type = readCard.GetString(2);
            colour = readCard.GetString(3);
            status = readCard.GetString(4);
            positionX = readCard.GetInt32(5);
            positionY = readCard.GetInt32(6);
        }
        connection.Close();
    }

    public void UpdateCard(string type, string update)
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "UPDATE card SET $type = $update WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$type", type);
        command.Parameters.AddWithValue("$update", update);
        command.Parameters.AddWithValue("$card_id", cardId);
        command.ExecuteNonQuery();

        connection.Close();
    }

    public void DeleteCard()
    {
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM card WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$card_id", cardId);
        command.ExecuteNonQuery();

        connection.Close();
    }

    public string GetCardData(string type)
    {
        switch (type.ToLower())
        {
            case "title":
                return title;
            case "type":
                return type;
            case "colour":
                return colour;
            case "status":
                return status;
            default:
                return "";
        }
    }

    public int GetCardPosition(string axis)
    {
        switch (axis.ToLower())
        {
            case "x":
                return positionX;
            case "y":
                return positionY;
            default:
                return 0;
        }
    }
}