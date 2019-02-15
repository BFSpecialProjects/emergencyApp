using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioSend
{
    class Program
    {
        static void Main(string[] args)
        {
            //account sid and token, account phone number
            const string accountSid = "ACb5ea38bc4c703627eef8b374dd1d204e";
            const string authToken  = "9e29b19d23fe9777aa4d23d75966cec3";
            const string number = "+19199483074";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                    //"t" for test to keep debugging cost down
                    body: "t",
                    from: new Twilio.Types.PhoneNumber(number),
                    to: new Twilio.Types.PhoneNumber("+19192748780"));

            Console.WriteLine(message.Sid);
        }//end main
    }//end class
}//end namespace
