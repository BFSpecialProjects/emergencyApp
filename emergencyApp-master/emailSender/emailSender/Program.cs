using GemBox.Email;
using GemBox.Email.Pop;
using System;
using System.Net.Mail;
using System.Threading;

//TODO: set up receiving from text messaging

namespace emailSender
{
    class Program
    {
        public static void sendEmail(string recipient)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                mail.From = new System.Net.Mail.MailAddress(Ref.email);
                mail.To.Add(recipient);
                mail.Body = "This is a test of NestWatch Alpha";
                mail.Subject = "BFSpecialProjects";

                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential(Ref.email, Ref.password);
                client.EnableSsl = true;

                client.Send(mail);
                Console.WriteLine("Email (text) sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }//end try-catch
        }//end method
        public static void checkEmail()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            using (PopClient pop = new PopClient("pop.gmail.com"))
            {
                //  Connect and sign to POP server.
                pop.Connect();
                pop.Authenticate(Ref.email, Ref.password);

                // Read the number of currently available emails on the server.
                int count = pop.GetCount();

                Console.WriteLine(" NO. |     DATE     |          MESSAGE          ");
                Console.WriteLine("------------------------------------------------");

                // Download and receive all email messages.
                try
                {
                    for (int number = 1; number <= count; number++)
                    {
                        GemBox.Email.MailMessage message = pop.GetMessage(number);

                        // Read and display email's date and subject.
                        Console.WriteLine($"  {number}  |  {message.Date.ToShortDateString()}  |  {message.BodyText}");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
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
            Thread sendEmail = new Thread(new ThreadStart(checkEmail));
            sendEmail.Start();

            //getEmail();
            Console.ReadLine();
        }//end main
    }//end class
}//end namespace
