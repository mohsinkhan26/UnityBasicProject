using UnityEngine;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using UnityEngine.UI;

//using System.Text;

/* NOTE: Make the FROM email less secure (Turn On) on 
 * https://www.google.com/settings/security/lesssecureapps
 * oterwise, it won't work
*/

namespace MK.Common.Utilities
{
    public class ReportEmail : MonoBehaviour
    {
        //[SerializeField]
        //Text status;

        string fromEmail = "crash.report@gmail.com";
        string toEmail = "project.report@gmail.com";
        string password = "12345678";
        string logs = "";
        //StringBuilder sbLogs = new StringBuilder(); // TODO: better to use it after testing

        public string Log
        {
            get
            {
                return logs;
            }
        }

        //void Start()
        //{
        //    #if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        //    fromEmail = fromEmail.Trim();
        //    toEmail = toEmail.Trim();
        //    password = password.Trim();
        //    if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(password))
        //    {
        //        Debug.LogError("From/To email or password is NULL");
        //        Debug.Log("System Information:\n" + GetSystemInformation());
        //    }
        //    else if (!IsValidEmail(fromEmail) || !IsValidEmail(toEmail))
        //    {
        //        Debug.LogError("From/To email is not valid");
        //        Debug.Log("System Information:\n" + GetSystemInformation());
        //    }
        //    else
        //        EmailToDevs("[PROJECT_NAME] - Report Email", "[PROJECT_NAME]\nSMTP mail from GMAIL");
        //    Debug.LogWarning("Mail-From: " + fromEmail + "   To: " + toEmail + "   Password: " + password);
        //    #endif
        //}

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            logs += logString + "\n" + stackTrace + "\n";
            //sbLogs.AppendLine(logString).AppendLine(stackTrace);
            //if (type == LogType.Error)
            //    Report();
        }

        public void Report()
        {
            EmailToDevs("[PROJECT_NAME] - Report Email", "[PROJECT_NAME]\nSMTP mail from GMAIL");
            //status.text = "Reported!";
        }

        public void EmailToDevs(string _subject, string _messageBody)
        {
            //#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

            smtpServer.Port = 587;

            smtpServer.EnableSsl = true;

            smtpServer.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(fromEmail, password);
            smtpServer.Credentials = (System.Net.ICredentialsByHost)credentials;

            try
            {
                MailMessage mail = new MailMessage();

                //For File Attachment, more files can also be attached
                //tested only for files on local machine
                //Attachment att = new Attachment(@"/*url of the file*/");
                //mail.Attachments.Add(att);

                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);

                mail.Subject = _subject;
                mail.Body = GetSystemInformation() + _messageBody + "\n" + logs;
                //mail.Body = GetSystemInformation() + _messageBody + "\n" + sbLogs.ToString();

                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smtpServer.Send(mail);
                Debug.Log("Success - Mail sent!");
                logs = "";
                //sbLogs = new StringBuilder();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Mail Sending Error: " + ex.Message + "\n\nStack: " + ex.StackTrace);
            }
            //#endif
        }

        bool IsValidEmail(string _emailaddress)
        { // http://stackoverflow.com/questions/5342375/regex-email-validation
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

        string GetSystemInformation()
        {
            string str = "*********************************************************";
            str += "\nOperating System: " + SystemInfo.operatingSystem;
            str += "\nDevice Model: " + SystemInfo.deviceModel;
            str += "\nDevice Type: " + SystemInfo.deviceType.ToString();
            str += "\nDevice Name: " + SystemInfo.deviceName;
            str += "\nDevice Unique Identifier: " + SystemInfo.deviceUniqueIdentifier;
            str += "\nProcessor Name: " + SystemInfo.processorType;
            str += "\nProcessor Frequency: " + SystemInfo.processorFrequency.ToString() + " MHz";
            str += "\nSystem Memory Size: " + SystemInfo.systemMemorySize.ToString() + " MB";
            str += "\nLogical Processors (number of hardware threads): " + SystemInfo.processorCount.ToString();
            str += "\nGraphics Device ID: " + SystemInfo.graphicsDeviceID.ToString();
            str += "\nGraphics Device Name: " + SystemInfo.graphicsDeviceName;
            str += "\nGraphics Device Type: " + SystemInfo.graphicsDeviceType.ToString();
            str += "\nGraphics Device Vendor: " + SystemInfo.graphicsDeviceVendor;
            str += "\nGraphics Device Vendor ID: " + SystemInfo.graphicsDeviceVendorID;
            str += "\nGraphics Device Version: " + SystemInfo.graphicsDeviceVersion;
            str += "\nGraphics Memory Size: " + SystemInfo.graphicsMemorySize.ToString();
            str += "\nGraphics Multi-Threaded: " + SystemInfo.graphicsMultiThreaded.ToString();
            str += "\nGraphics Shader Level: " + SystemInfo.graphicsShaderLevel.ToString();
            str += "\nMax Texture Size: " + SystemInfo.maxTextureSize.ToString();
            str += "\nNPOT (non-power of two size) texture support: " + SystemInfo.npotSupport.ToString();
            str += "\nCopy Texture Support: " + SystemInfo.copyTextureSupport.ToString();
            str += "\nSupported Render Target Count: " + SystemInfo.supportedRenderTargetCount.ToString() + " MRTs";
            str += "\nSupports 2D Array Textures: " + SystemInfo.supports2DArrayTextures.ToString();
            str += "\nSupports 3D Textures: " + SystemInfo.supports3DTextures.ToString();
            str += "\nSupports Accelerometer: " + SystemInfo.supportsAccelerometer.ToString();
            str += "\nSupports Audio: " + SystemInfo.supportsAudio.ToString();
            str += "\nSupports Compute Shaders: " + SystemInfo.supportsComputeShaders.ToString();
            str += "\nSupports Gyroscope: " + SystemInfo.supportsGyroscope.ToString();
            str += "\nSupports Image Effects: " + SystemInfo.supportsImageEffects.ToString();
            str += "\nSupports Instancing: " + SystemInfo.supportsInstancing.ToString();
            str += "\nSupports Location Service: " + SystemInfo.supportsLocationService.ToString();
            str += "\nSupports Motion Vectors: " + SystemInfo.supportsMotionVectors.ToString();
            str += "\nSupports Raw Shadow Depth Sampling: " + SystemInfo.supportsRawShadowDepthSampling.ToString();
            //str += "\nSupports Render Textures: " + SystemInfo.supportsRenderTextures.ToString();
            str += "\nSupports Render To Cubemap: " + SystemInfo.supportsRenderToCubemap.ToString();
            str += "\nSupports Shadows: " + SystemInfo.supportsShadows.ToString();
            str += "\nSupports Sparse Textures: " + SystemInfo.supportsSparseTextures.ToString();
            //str += "\nSupports Stencil: " + SystemInfo.supportsStencil.ToString();
            str += "\nSupports Vibration: " + SystemInfo.supportsVibration.ToString();
            str += "\nUnsupported Identifier: " + SystemInfo.unsupportedIdentifier;
            str += "\n*********************************************************";
            return (str + "\n\n\n\n");
        }
    }
}
