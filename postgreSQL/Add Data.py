import psycopg2
from psycopg2 import Error

arrA = [1, 3, 5, 7, 9, 11]
arrB = [2, 4, 6, 8, 10, 12]
arrC = ["one", "two", "three", "four", "five", "six"]
lenA = len(arrA)
lenB = len(arrB)
arrA.insert(lenA + 1, arrA[lenA - 1] + 2)
print(arrA)
arrB.append(arrB[lenB - 1] + 2)
print(arrB)

try:
    connection = psycopg2.connect(host="localhost",
                                  database="postgres",
                                  port="5432",
                                  user="postgres",
                                  password="pass")
    cursor = connection.cursor()
    print(connection.get_dsn_parameters(), "\n")
    cursor.execute("SELECT version();")
    record = cursor.fetchone()
    print("You are connected to - ", record, "\n")

    cursor.execute("INSERT INTO Teachers VALUES(1, 'John Doe', 'jdoe@wcpss.net', '1234567')")
    cursor.execute("INSERT INTO Teachers VALUES(2, 'Jane Doe', 'jdoe2@wcpss.net', '7654321')")

    cursor.execute("INSERT INTO Arrays VALUES(1, arrA)")
    cursor.execute("INSERT INTO Arrays VALUES(2, arrB)")
    connection.commit()
    print("Values inserted successfully.")

except (Exception, psycopg2.Error) as error:
    print("Error while connecting to PostgreSQL", error)
finally:
        if(connection):
            cursor.close()
            connection.close()
            print("PostgreSQL connection is closed")
