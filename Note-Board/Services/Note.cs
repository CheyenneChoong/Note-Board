using Microsoft.Data.Sqlite;

namespace Note_Board.Models;
public class Note : Card
{
    private int cardId;
    private int noteId;
    private string note;

    public Note(int id) : base(id)
    {
        cardId = id;
        using var connection = new SqliteConnection("Data Source=database.db");
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM note WHERE card_id = $card_id;";
        command.Parameters.AddWithValue("$card_id", cardId);

        using var readNote = command.ExecuteReader();
        noteId = readNote.GetInt32(0);
        note = readNote.GetString(2);

        connection.Close();
    }
}