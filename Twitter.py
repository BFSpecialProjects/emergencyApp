import tweepy

status = "App Status Here"
status_temp = status

consumer_key ="xxxxxxxxxxxxxxxx"
consumer_secret ="xxxxxxxxxxxxxxxx"
access_token ="xxxxxxxxxxxxxxxx"
access_token_secret ="xxxxxxxxxxxxxxxx"

auth = tweepy.OAuthHandler(consumer_key, consumer_secret)
auth.set_access_token(access_token, access_token_secret)
api = tweepy.API(auth)
tweet = status

if status != status_temp:
    status_temp = status
    api.update_status(status=tweet)
