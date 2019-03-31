import psycopg2
conn = psycopg2.connect("host=localhost dbname=postgres port=5432 user=postgres password=nicetry")
cur = conn.cursor()

cur.execute("INSERT INTO Teachers VALUES (%s, %s, %s, %s)", (1, 'John Done', 'jdoe@wcpss.net', '1234567'))
cur.execute("INSERT INTO Teachers VALUES (%s, %s, %s, %s)", (2, 'John Doe', 'jdoe@wcpss.net', '1234567'))
cur.execute("INSERT INTO Teachers VALUES (%s, %s, %s, %s)", (3, 'Jane Doe', 'jdoe@wcpss.net', '7654321'))
conn.commit()
