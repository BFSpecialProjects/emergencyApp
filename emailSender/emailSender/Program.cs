using System;
using System.Net.Mail;

//TODO: set up receiving from text messaging

namespace emailSender
{
    class Program
    {
        public static void sendEmail(string recipient)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("bfspecialprojects@gmail.com");
                mail.To.Add(recipient);
                mail.Body = "This is a test of NestWatch Alpha";

                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential("bfspecialprojects@gmail.com", "Black4Falcons!");
                client.EnableSsl = true;

                client.Send(mail);
                Console.WriteLine("Email (text) sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }//end try-catch
        }//end method
        public static void getEmail()
        {
            Console.WriteLine("Enter number (no dashes): ");
            string number = Console.ReadLine();

            Console.WriteLine("Enter Carrier: ");
            string carrier = Console.ReadLine();

            if (carrier == "Verizon")
            {
                string numEmail = number + "@vtext.com";
                sendEmail(numEmail);
            }
            else if (carrier == "T-Mobile")
            {
                string numEmail = number + "@tmomail.net";
                sendEmail(numEmail);
            }
            else
            {
                Console.WriteLine("DEBUG: Verizon only, please.");
            }//end if
        }//end method

        static void Main(string[] args)
        {
            getEmail();
            Console.ReadLine();
        }//end main
    }//end class
}//end namespace
