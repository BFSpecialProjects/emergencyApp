# can be run in python 2.7, no need to use alias
# transfer to and run on desktop

from flask import Flask, request, redirect
from twilio.twiml.messaging_response import MessagingResponse

app = Flask(__name__)

@app.route("/sms", methods = ['GET', 'POST'])

def sms_reply():
	try:
		# get message sent by user
		body = request.values.get('Body', None)

		# start TwiML response
		resp = MessagingResponse()

		# determine reply
		if body == 'a':
			resp.message('1')
		elif body == 'b':
			resp.message('2')

		return str(resp)
	except TwilioRestException as e:
		print(e)

if __name__ == "__main__":
	app.run(debug=True)
