/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using System;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using UnityEngine;

/* NOTE: Make the FROM email less secure (Turn On) on
 * https://www.google.com/settings/security/lesssecureapps otherwise, it won't work. DEPRECATED!
 *
 * For Gmail, you need to generate App Password after 2-Step Verification https://support.google.com/accounts/answer/185833
 *
 * For Microsoft Outlook, you need to generate App Password https://www.youtube.com/watch?v=0gjsIDNJx3M&t=74s
 */

namespace MK.Common.Utilities
{
    public sealed class OTPEmail : MonoBehaviour
    {
        [SerializeField] string projectName = "PROJECT_NAME";
        [SerializeField] string subjectOfEmail = "Verify your login with OTP";
        [SerializeField] string fromEmail = "fromEmail@gmail.com";
        [SerializeField] string password = "0123456789123456"; // App password generated for fromEmail

        [Header("For Testing")] [SerializeField]
        private string toEmailForTesting = "toEmail@gmail.com";

        [SerializeField] string otpForTesting = "123456";

        public void SendEmailWithOTP(string _toEmail, string _OTP, ulong unixTimeInMilliseconds = 0)
        {
            if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(_toEmail) || string.IsNullOrEmpty(password))
            {
                Debug.LogException(new Exception("EmailWithOTP can't possible, emails or password not provided"));
                return;
            }

            if (!IsValidEmail(_toEmail))
            {
                Debug.LogException(new Exception("EmailWithOTP can't possible, receiver email is not valid"));
                return;
            }

            EmailWithOTP(_toEmail, OTPEmailBody(_OTP, unixTimeInMilliseconds));
        }

        private void EmailWithOTP(string _toEmail, string _messageBody)
        {
            //#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN

            try
            {
                // This is simply to get the elapsed time for this phase of AssetLoading.
                float startTime = Time.realtimeSinceStartup;
                using (MailMessage mail = new MailMessage())
                {
                    // SmtpClient smtpServer = new SmtpClient("smtp.gmail.com") // for Gmail
                    SmtpClient smtpServer = new SmtpClient("smtp-mail.outlook.com") // for Microsoft Outlook
                    {
                        Port = 587,
                        EnableSsl = true,
                        UseDefaultCredentials = false,
                        Credentials = (System.Net.ICredentialsByHost) new NetworkCredential(fromEmail, password)
                    };

                    //smtpServer.Port = 587;

                    //smtpServer.EnableSsl = true;

                    //smtpServer.UseDefaultCredentials = false;
                    //NetworkCredential credentials = new NetworkCredential(fromEmail, password);
                    //smtpServer.Credentials = (System.Net.ICredentialsByHost)credentials;

                    //MailMessage mail = new MailMessage();

                    //For File Attachment, more files can also be attached
                    //tested only for files on local machine
                    //Attachment att = new Attachment(@"/*url of the file*/");
                    //mail.Attachments.Add(att);

                    mail.From = new MailAddress(fromEmail);
                    mail.To.Add(_toEmail);

                    mail.Subject = new StringBuilder(projectName).Append(" - ").Append(subjectOfEmail).ToString();
                    mail.Body = _messageBody;
                    mail.IsBodyHtml = true;

                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate(object s, X509Certificate certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                    smtpServer.Send(mail);
                }

                // Calculate and display the elapsed time.
                float elapsedTime = Time.realtimeSinceStartup - startTime;
                Debug.Log("Success - Mail sent! Time taken in sending email: " + elapsedTime + " sec");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Mail Sending Error: " + ex.Message + "\n\nStack: " + ex.StackTrace);
            }

            //#endif
        }

        bool IsValidEmail(string _emailaddress)
        {
            // http://stackoverflow.com/questions/5342375/regex-email-validation
            try
            {
                MailAddress m = new MailAddress(_emailaddress);

                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }

        string OTPEmailBody(string _OTP, ulong unixTimeInMilliseconds)
        {
            // Create OTP email sample HTML code https://codepen.io/abhndv/pen/rNebjGo
            StringBuilder body = new StringBuilder(); // header
            // "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">"
            // + "<html xmlns=\"http://www.w3.org/1999/xhtml\">"
            // + "<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">"
            // + "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
            // + "</head><body style=\"font-family: Helvetica, Arial, sans-serif; margin: 0px; padding: 0px; background-color: #ffffff;\">");
            body.Append(
                "<div style=\"font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2\">"
                + "<div style=\"margin:50px auto;width:70%;padding:20px 0\">"
                + "<div style=\"border-bottom:1px solid #eee\">"
                + "<a href=\"https://www.hksyu.edu/en/home\" style=\"font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600\">EduFarm</a></div>"
                + "<p style=\"font-size:1.1em\">Dear,</p>"
                + "<p>Thank you for choosing EduFarm. Use the following OTP to complete your Login. OTP is valid for 5 minutes</p>"
                + "<h2 style=\"background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;\">");
            body.Append(_OTP);
            body.Append("</h2>");
            body.Append(CountDownTimer(unixTimeInMilliseconds)); // it won't work in most email clients
            body.Append("<p style=\"font-size:0.9em;\">Regards,<br />EduFarm</p>"
                        + "<hr style=\"border:none;border-top:1px solid #eee\" />"
                        + "<div style=\"float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300\">"
                        + "<p>Hong Kong Shue Yan University</p> <p>10 Wai Tsui Cres, North Point</p>"
                        + "<p>Hong Kong</p> </div> </div> </div>");
            // body.Append("</body> </html>"); // footer
            return body.ToString();
        }

        // Reference: https://stackoverflow.com/questions/12664481/java-scripts-not-working-in-html-email-template
        // NOTE: it won't work in most email clients
        string CountDownTimer(ulong unixTimeInMilliseconds)
        {
            if (unixTimeInMilliseconds == 0) return "";

            // Reference: https://www.educative.io/answers/how-to-create-a-countdown-timer-using-javascript
            StringBuilder timerHTML = new StringBuilder();
            // timerHTML.Append("<iframe src=\"\" height=\"200\" width=\"200\" title=\"Iframe Timer\">");
            timerHTML.Append("<div style=\"display: inline; font-size: 18px; margin-top: 0px;\">"
                             + "<h3 style=\"background: #98e2f1;margin:5px auto;" +
                             "width:max-content;padding:0 10px;color: #575757;" +
                             "border-radius: 10px;\"></h3></div>");
            timerHTML.Append("<script> var countDownDate = new Date(");
            timerHTML.Append(unixTimeInMilliseconds); // The data/time we want to countdown to
            timerHTML.Append(").getTime();var myfunc = setInterval(function() {" // Run myfunc every second
                             + "const h3Tag = document.getElementsByTagName(\"h3\")[0];"
                             + "var now = new Date().getTime();var timeleft = countDownDate - now;");
            // Calculating the days, hours, minutes and seconds left
            timerHTML.Append("var days = Math.floor(timeleft / (1000 * 60 * 60 * 24));"
                             + "var hours = Math.floor((timeleft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));"
                             + "var minutes = Math.floor((timeleft % (1000 * 60 * 60)) / (1000 * 60));"
                             + "var seconds = Math.floor((timeleft % (1000 * 60)) / 1000);");
            // Result is output to the specific element
            timerHTML.Append("h3Tag.innerHTML = days + \"d \" + hours + \"h \" + minutes + \"m \" + seconds + \"s \";");
            // Display the message when countdown is over
            timerHTML.Append("if (timeleft < 0) { clearInterval(myfunc);"
                             + "h3Tag.innerHTML = \"TIME UP!!\";"
                             + "} }, 1000); </script>");
            // timerHTML.Append("</iframe>");
            return timerHTML.ToString();
        }

        [ContextMenu("Test OTP Email")]
        void TestOTPEmail()
        {
#if UNITY_EDITOR
            SendEmailWithOTP(toEmailForTesting, otpForTesting, 1688589947426);
#endif
        }
    }
}
