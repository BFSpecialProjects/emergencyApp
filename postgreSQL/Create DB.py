import psycopg2
conn = psycopg2.connect("host=localhost dbname=postgres port=5432 user=postgres password=nicetry")
cur = conn.cursor()

# cur.execute(command)
cur.execute("""
    CREATE TABLE Test(
    ID_Number integer PRIMARY KEY,
    ArrayA integer,
    ArrayB integer)
""")
conn.commit()

cur.execute("""
   CREATE TABLE Teachers(
    ID_Number integer PRIMARY KEY,
    Name text,
    Email text,
    Phone_Number text)
    """)
conn.commit()
