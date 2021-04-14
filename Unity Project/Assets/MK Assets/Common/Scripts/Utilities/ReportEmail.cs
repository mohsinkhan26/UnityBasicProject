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
 * https://www.google.com/settings/security/lesssecureapps
 * oterwise, it won't work
*/

namespace MK.Common.Utilities
{
    public class ReportEmail : MonoBehaviour
    {
        const string PROJECT_NAME = "PROJECT_NAME";
        string fromEmail = "crash.report@gmail.com";
        string toEmail = "project.report@gmail.com";
        string password = "12345678"; // password of fromEmail
        StringBuilder logs = new StringBuilder();

        float gameStartTime;
        int maxLimit = Int32.MaxValue - (Int32.MaxValue / 10000);

        public string Log
        {
            get { return logs.ToString(); }
        }

        void OnEnable()
        {
            //Debug.Log(GetSystemInformation()); // TODO: just for testing
            gameStartTime = Time.realtimeSinceStartup;
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            try
            {
                // minimise Log
                if (logs.Length + logString.Length >= maxLimit)
                    logs.Remove(0, maxLimit);

                logs.AppendLine(logString).AppendLine(stackTrace);
            }
            catch (Exception _ex)
            {
                Debug.LogException(_ex, this);
                ClearLogs();
                HandleLog(logString, stackTrace, type);
            }
        }

        void ClearLogs()
        {
            logs.Remove(0, logs.Length);
            gameStartTime = Time.realtimeSinceStartup;
        }

        public void Report()
        {
            EmailToDevs("[" + PROJECT_NAME + "] - Report Email", "[" + PROJECT_NAME + "]\nSMTP mail from GMAIL");
        }

        public void Report(string _query)
        {
            EmailToDevs("[" + PROJECT_NAME + "] - Report Email", "Query: " + _query
                                                                           + "\n\n[" + PROJECT_NAME +
                                                                           "] SMTP mail from GMAIL\n\n");
        }

        public void Report(string _userName, string _password, string _email, string _query)
        {
            EmailToDevs("[" + PROJECT_NAME + "] - Report Email",
                GetUserInformation(_userName, _password, _email).Append("Query: ").AppendLine(_query).ToString()
                + "\n\n[" + PROJECT_NAME + "] SMTP mail from GMAIL\n\n");
        }

        public void EmailToDevs(string _subject, string _messageBody)
        {
            //#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
            if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(password))
            {
                Debug.LogException(new Exception("EmailToDevs can't possible, emails or password not provided"));
                return;
            }

            // Calculate and display the elapsed time.
            float gameElapsedTime = Time.realtimeSinceStartup - gameStartTime;

            try
            {
                // This is simply to get the elapsed time for this phase of AssetLoading.
                float startTime = Time.realtimeSinceStartup;
                using (MailMessage mail = new MailMessage())
                {
                    SmtpClient smtpServer = new SmtpClient("smtp.gmail.com")
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
                    mail.To.Add(toEmail);

                    mail.Subject = _subject;
                    mail.Body = GetSystemInformation()
                        .Append("Logs of Game Time, till now: ").Append(gameElapsedTime.ToString()).AppendLine(" sec")
                        .Append(_messageBody).AppendLine()
                        .Append(logs.ToString()).ToString();

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
                ClearLogs();
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

        StringBuilder GetUserInformation(string _userName, string _password, string _email)
        {
            StringBuilder str = new StringBuilder("*********************************************************");
            str.Append("\nUsername: ").Append(_userName);
            str.Append("\nPassword: ").Append(_password);
            str.Append("\nEmail: ").Append(_email);
            str.Append("\n*********************************************************");
            return str.AppendLine();
        }

        StringBuilder GetSystemInformation()
        {
            StringBuilder str = new StringBuilder("*********************************************************");
            str.Append("\nProduct Name: ").Append(Application.productName);
            str.Append("\nIdentifier: ").Append(Application.identifier);
            str.Append("\nPlatform: ").Append(Application.platform.ToString());
            str.Append("\nVersion: ").Append(Application.version);
            //#if UNITY_ANDROID // can't work at runtime
            //            str.Append("\nBuild: ").Append(PlayerSettings.Android.bundleVersionCode);
            //#elif UNITY_IOS || UNITY_IPHONE
            //            str.Append("\nBuild: ").Append(PlayerSettings.iOS.buildNumber);
            //#elif UNITY_STANDALONE_OSX
            //            str.Append("\nBuild: ").Append(PlayerSettings.macOS.buildNumber);
            //#endif
            str.Append("\n\nOperating System: ").Append(SystemInfo.operatingSystem);
            str.Append("\nUnity Version: ").Append(Application.unityVersion);
            str.Append("\nTarget Frame Rate: ").Append(Application.targetFrameRate);
            str.Append("\nDevice Model: ").Append(SystemInfo.deviceModel);
            str.Append("\nDevice Type: ").Append(SystemInfo.deviceType.ToString());
            str.Append("\nDevice Name: ").Append(SystemInfo.deviceName);
            str.Append("\nDevice Unique Identifier: ").Append(SystemInfo.deviceUniqueIdentifier);
            str.Append("\nProcessor Name: ").Append(SystemInfo.processorType);
            str.Append("\nProcessor Frequency: ").Append(SystemInfo.processorFrequency.ToString()).Append(" MHz");
            str.Append("\nSystem Memory Size: ").Append(SystemInfo.systemMemorySize.ToString()).Append(" MB");
            str.Append("\nLogical Processors (number of hardware threads): ")
                .Append(SystemInfo.processorCount.ToString());
            str.Append("\nGraphics Device ID: ").Append(SystemInfo.graphicsDeviceID.ToString());
            str.Append("\nGraphics Device Name: ").Append(SystemInfo.graphicsDeviceName);
            str.Append("\nGraphics Device Type: ").Append(SystemInfo.graphicsDeviceType.ToString());
            str.Append("\nGraphics Device Vendor: ").Append(SystemInfo.graphicsDeviceVendor);
            str.Append("\nGraphics Device Vendor ID: ").Append(SystemInfo.graphicsDeviceVendorID);
            str.Append("\nGraphics Device Version: ").Append(SystemInfo.graphicsDeviceVersion);
            str.Append("\nGraphics Memory Size: ").Append(SystemInfo.graphicsMemorySize.ToString());
            str.Append("\nGraphics Multi-Threaded: ").Append(SystemInfo.graphicsMultiThreaded.ToString());
            str.Append("\nGraphics Shader Level: ").Append(SystemInfo.graphicsShaderLevel.ToString());
            str.Append("\nMax Texture Size: ").Append(SystemInfo.maxTextureSize.ToString());
            str.Append("\nNPOT (non-power of two size) texture support: ").Append(SystemInfo.npotSupport.ToString());
            str.Append("\nCopy Texture Support: ").Append(SystemInfo.copyTextureSupport.ToString());
            str.Append("\nSupported Render Target Count: ").Append(SystemInfo.supportedRenderTargetCount.ToString())
                .Append(" MRTs");
            str.Append("\nSupports 2D Array Textures: ").Append(SystemInfo.supports2DArrayTextures.ToString());
            str.Append("\nSupports 3D Textures: ").Append(SystemInfo.supports3DTextures.ToString());
            str.Append("\nSupports Accelerometer: ").Append(SystemInfo.supportsAccelerometer.ToString());
            str.Append("\nSupports Audio: ").Append(SystemInfo.supportsAudio.ToString());
            str.Append("\nSupports Compute Shaders: ").Append(SystemInfo.supportsComputeShaders.ToString());
            str.Append("\nSupports Gyroscope: ").Append(SystemInfo.supportsGyroscope.ToString());
            str.Append("\nSupports Image Effects: ").Append(SystemInfo.supportsImageEffects.ToString());
            str.Append("\nSupports Instancing: ").Append(SystemInfo.supportsInstancing.ToString());
            str.Append("\nSupports Location Service: ").Append(SystemInfo.supportsLocationService.ToString());
            str.Append("\nSupports Motion Vectors: ").Append(SystemInfo.supportsMotionVectors.ToString());
            str.Append("\nSupports Raw Shadow Depth Sampling: ")
                .Append(SystemInfo.supportsRawShadowDepthSampling.ToString());
            str.Append("\nSupports Render Textures: ").Append(SystemInfo.supportsRenderTextures.ToString());
            str.Append("\nSupports Render To Cubemap: ").Append(SystemInfo.supportsRenderToCubemap.ToString());
            str.Append("\nSupports Shadows: ").Append(SystemInfo.supportsShadows.ToString());
            str.Append("\nSupports Sparse Textures: ").Append(SystemInfo.supportsSparseTextures.ToString());
            str.Append("\nSupports Stencil: ").Append(SystemInfo.supportsStencil.ToString());
            str.Append("\nSupports Vibration: ").Append(SystemInfo.supportsVibration.ToString());
            str.Append("\nUnsupported Identifier: ").Append(SystemInfo.unsupportedIdentifier);
            str.Append("\n*********************************************************");
            return str.AppendLine().AppendLine();
        }
    }
}
