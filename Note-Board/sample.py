import sqlite3

connect = sqlite3.connect("Note-Board/database.db")
cursor = connect.cursor()
for x in range(1, 5):
    cursor.execute("""INSERT INTO card (title, description, type, colour, status, position_x, position_y)
                    VALUES ('Test', 'Test description', 'Note', '#FFF697', 'active', 200, 200);""")
    connect.commit()

