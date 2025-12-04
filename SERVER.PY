import socket
import json
import pyodbc
from datetime import datetime

HOST = '127.0.0.1'
PORT = 9999

def get_connection():
    return pyodbc.connect(
        'DRIVER={ODBC Driver 17 for SQL Server};'
        'SERVER=localhost\\SQLEXPRESS;'
        'DATABASE=DBSAdmissions;'
        'Trusted_Connection=yes;'
    )

def generate_registration_number():
    return f"DBS-{datetime.now().strftime('%Y%m%d%H%M%S')}"

def handle_client(sock):
    print(" handle_client triggered")
    try:
        raw = sock.recv(4096)
        if not raw:
            print(" No data received.")
            sock.sendall(json.dumps({'status': 'error', 'message': 'No data received'}).encode('utf-8'))
            return

        data = raw.decode('utf-8')
        print(f" Received raw data: {data}")

        try:
            applicant = json.loads(data)
        except json.JSONDecodeError as je:
            print(f" JSON error: {je}")
            sock.sendall(json.dumps({'status': 'error', 'message': 'Invalid JSON'}).encode('utf-8'))
            return

        print(f" Parsed applicant: {applicant}")

        #  FIXED: use 'qualification' instead of 'qualifications'
        name = applicant['name']
        address = applicant['address']
        qualification = applicant['qualification']  #  correct key
        course = applicant['course']
        start_year = int(applicant['start_year'])
        start_month = int(applicant['start_month'])
        reg_number = generate_registration_number()

        try:
            conn = get_connection()
            cursor = conn.cursor()
            print(" Executing INSERT...")
            cursor.execute('''
                INSERT INTO Applications (Name, Address, Qualifications, Course, StartYear, StartMonth, RegistrationNumber)
                VALUES (?, ?, ?, ?, ?, ?, ?)
            ''', (
                name, address, qualification, course, start_year, start_month, reg_number
            ))
            conn.commit()
            conn.close()
            print(" Insert successful.")
            sock.sendall(json.dumps({'status': 'success', 'registration_number': reg_number}).encode('utf-8'))
        except Exception as db_err:
            print(f" Database error: {db_err}")
            sock.sendall(json.dumps({'status': 'error', 'message': f'Database error: {db_err}'}).encode('utf-8'))

    except Exception as e:
        print(f" Server error: {e}")
        sock.sendall(json.dumps({'status': 'error', 'message': f'Server error: {e}'}).encode('utf-8'))
    finally:
        sock.close()

def start_server():
    srv = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    srv.bind((HOST, PORT))
    srv.listen(5)
    print(f" Server listening on {HOST}:{PORT}")

    while True:
        conn, addr = srv.accept()
        print(f" Connected by {addr}")
        handle_client(conn)

if __name__ == '__main__':
    start_server()
