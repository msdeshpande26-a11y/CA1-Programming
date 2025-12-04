import socket
import json

HOST = '127.0.0.1'
PORT = 9999

def collect_applicant_data():
    print(">>> DBS Admissions Client")
    name = input("Enter full name: ").strip()
    address = input("Enter address: ").strip()
    qualification = input("Enter educational qualification: ").strip()

    print("Courses available:")
    print("1. MSc in Cyber Security")
    print("2. MSc Information Systems & computing")
    print("3. MSc Data Analytics")
    course_choice = input("Enter course number (1-3): ").strip()

    courses = {
        '1': 'MSc in Cyber Security',
        '2': 'MSc Information Systems & computing',
        '3': 'MSc Data Analytics'
    }

    #  Validate course choice
    if course_choice not in courses:
        print(" Invalid course selection. Please enter 1, 2, or 3.")
        return None
    course = courses[course_choice]

    start_year = input("Enter intended start year (e.g., 2025): ").strip()

    #  Validate year (must be 4 digits and numeric)
    if not (start_year.isdigit() and len(start_year) == 4):
        print(" Invalid year. Please enter a 4-digit year like 2025.")
        return None

    start_month = input("Enter intended start month (1-12): ").strip()

    #  Validate month (must be between 1 and 12)
    if not (start_month.isdigit() and 1 <= int(start_month) <= 12):
        print(" Invalid month. Please enter a number between 1 and 12.")
        return None

    return {
        'name': name,
        'address': address,
        'qualification': qualification,
        'course': course,
        'start_year': start_year,
        'start_month': start_month
    }

def send_application():
    applicant_data = collect_applicant_data()
    if applicant_data is None:
        print(" Application not sent due to invalid input.")
        return

    message = json.dumps(applicant_data).encode('utf-8')

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as client:
        client.connect((HOST, PORT))
        client.sendall(message)
        print(" Sent data to server:", applicant_data)

        response = client.recv(4096).decode('utf-8')
        result = json.loads(response)

        if result['status'] == 'success':
            print(f"\n Application submitted successfully!")
            print(f"Your registration number is: {result['registration_number']}")
        else:
            print(f"\n Submission failed: {result['message']}")

if __name__ == '__main__':
    send_application()
