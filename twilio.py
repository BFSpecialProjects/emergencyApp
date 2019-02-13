# Install python 3.7 (make sure you also install pip and add to path, can do both during install)
# Open PowerShell, type pip install twilio (you can create a venv via virtualenv if you want to, not required)
# Run following code (VS Code or PowerShell both work well if you dont have an IDE)
from twilio.rest import Client
# Create trial account and replace placeholder text
client = Client("ACb5ea38bc4c703627eef8b374dd1d204e", "9e29b19d23fe9777aa4d23d75966cec3")
# fill in to(verified nums only during trial), to (the number you chose) and body(message)
client.messages.create(to="enter to number (verified numbers only during trial period)",
                       from_="+19199483074",
                       body="Message Here")
