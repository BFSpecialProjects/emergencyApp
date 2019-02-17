# can be run in python 2.7, no need to use alias
# TODO: transfer to and run on desktop

from twilio.rest import Client

# account SID
account_sid = "ACb5ea38bc4c703627eef8b374dd1d204e"

# account auth token
auth_token = "9e29b19d23fe9777aa4d23d75966cec3"

# client object
client = Client(account_sid, auth_token)

# create and send message
message = client.messages.create(
	to = "+19192748780",
	from_ = "+19199483074",
	body = "t")

print(message.sid)